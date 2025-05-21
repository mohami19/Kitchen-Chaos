using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CutttingRecipeSO")]
public class CutttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;

}

