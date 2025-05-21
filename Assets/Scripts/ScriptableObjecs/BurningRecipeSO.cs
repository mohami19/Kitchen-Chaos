using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;

}

