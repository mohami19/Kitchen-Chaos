using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;
    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
    }

    private void Player_OnAnyPlayerSpawned(object sender, EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChange -= Player_OnSelectedCounterChange;
            Player.LocalInstance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
        }
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visual in visualGameObject)
        {
            visual.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visual in visualGameObject)
        {
            visual.SetActive(false);
        }
    }
}
