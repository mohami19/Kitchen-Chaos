using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
