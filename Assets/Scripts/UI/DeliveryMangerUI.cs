using UnityEngine;

public class DeliveryMangerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private Transform recipeTemplate;


    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in recipeContainer)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, recipeContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryMangerSingleUI>().SetRecipeSO(recipeSO);
            // recipeTransform.GetComponent<RecipeSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
