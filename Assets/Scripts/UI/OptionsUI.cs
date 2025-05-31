using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{

    public static OptionsUI Instance { get; private set; }


    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;
        if (Instance != null && Instance != this)
        {
            Debug.LogError("More than one OptionsUI in the scene!");
            Destroy(gameObject);
            return;
        }


        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisuals();
        });

        CloseButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisuals();

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisuals()
    {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "MusicText: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}