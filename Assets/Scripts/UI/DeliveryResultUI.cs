using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{

    private const string POPUP = "Popup";



    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color SuccessColor;
    [SerializeField] private Sprite sucessSprite;

    [SerializeField] private Color FailedColor;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Hide();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        Show();

        animator.SetTrigger(POPUP);

        backgroundImage.color = SuccessColor;
        iconImage.sprite = sucessSprite;
        messageText.text = "Recipe\nDelivered";
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        Show();

        animator.SetTrigger(POPUP);

        backgroundImage.color = FailedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "Delivey\nFailed";

    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
