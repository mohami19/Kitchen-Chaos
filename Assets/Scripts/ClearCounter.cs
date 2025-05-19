using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Ineract(Player player)
    {
        Debug.Log("ClearCounter Ineract");
    }


}
