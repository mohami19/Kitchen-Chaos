using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class DeliveryManager : NetworkBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeList;

    private float spawnRecipeTimer = 4f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one DeliveryManager in the scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer) return;

        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeList.Count < waitingRecipeMax)
            {
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);

                SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);

            }
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[waitingRecipeSOIndex];

        waitingRecipeList.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool allIngredientsMatch = true;
                foreach (KitchenObjectSO kitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    if (!plateKitchenObject.GetKitchenObjectSOList().Contains(kitchenObjectSO))
                    {
                        allIngredientsMatch = false;
                        break;
                    }
                }

                if (allIngredientsMatch)
                {
                    DeliverCorrectRecipeServerRpc(i);

                    return;
                }
            }
        }

        DeliverInCorrectRecipeServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverInCorrectRecipeServerRpc()
    {
        DeliverInCorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverInCorrectRecipeClientRpc()
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeListIndex)
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeListIndex);
    }


    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeListIndex)
    {
        successfulRecipesAmount++;

        waitingRecipeList.RemoveAt(waitingRecipeListIndex);

        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeList()
    {
        return waitingRecipeList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
