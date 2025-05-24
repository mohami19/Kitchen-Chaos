using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            if (kitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                kitchenObject.DestroySelf();
            }
        }
        else
        {
            Debug.Log("You need to hold a kitchen object to deliver.");
        }
    }
}