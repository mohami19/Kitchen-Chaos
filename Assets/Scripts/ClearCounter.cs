using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    public void Ineract()
    {
        Debug.Log("Interacting with " + gameObject.name);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
