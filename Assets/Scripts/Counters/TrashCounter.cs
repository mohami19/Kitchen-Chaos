public class TrashCounter : BaseCounter
{
    public override void Ineract(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
