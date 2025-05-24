using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one DeliveryCounter in the scene!");
            Destroy(gameObject);
            return;
        }
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            if (kitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                kitchenObject.DestroySelf();
            }
        }
        else
        {
            Debug.Log("You need to hold a kitchen object to deliver.");
        }
    }
}