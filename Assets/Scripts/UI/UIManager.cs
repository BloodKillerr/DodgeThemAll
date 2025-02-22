using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<IconDictionaryEntry> keyboardIcons;
    [SerializeField] private List<IconDictionaryEntry> xBoxIcons;
    [SerializeField] private List<IconDictionaryEntry> psIcons;

    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text currentMaxText;

    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private CanvasGroup pauseMenu;
    [SerializeField] private CanvasGroup controlsMenu;
    [SerializeField] private CanvasGroup pauseControlsMenu;
    [SerializeField] private CanvasGroup restartMenu;

    public UnityEvent IconsUpdateEvent;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PointSystem.Instance.PointSystemUpdateEvent.AddListener(UpdatePointSystemUI);
        currentMaxText.text = "Highscore: " + PointSystem.Instance.MaxPoints.ToString();
    }

    public Sprite GetCorrectIcon(SequenceManager.SequenceItem item)
    {
        switch (InputManager.Instance.LastUsedDevice)
        {
            case "Keyboard":
                return keyboardIcons.Find(i => i.key == item.ToString()).value;
            case "XBox":
                return xBoxIcons.Find(i => i.key == item.ToString()).value;
            case "PlayStation":
                return psIcons.Find(i => i.key == item.ToString()).value;
            default:
                return keyboardIcons.Find(i => i.key == item.ToString()).value;
        }
    }

    public void UpdatePointSystemUI()
    {
        pointsText.text = "Points: " + PointSystem.Instance.Points.ToString();
        currentMaxText.text = "Highscore: " + PointSystem.Instance.MaxPoints.ToString();
    }

    public void ShowMainMenu()
    {
        mainMenu.alpha = 1f;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;
    }

    public void HideMainMenu()
    {
        mainMenu.alpha = 0f;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.alpha = 1f;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;
    }

    public void HidePauseMenu()
    {
        pauseMenu.alpha = 0f;
        pauseMenu.interactable = false;
        pauseMenu.blocksRaycasts = false;
    }

    public void ShowControlsMenu()
    {
        controlsMenu.alpha = 1f;
        controlsMenu.interactable = true;
        controlsMenu.blocksRaycasts = true;
    }

    public void HideControlsMenu()
    {
        controlsMenu.alpha = 0f;
        controlsMenu.interactable = false;
        controlsMenu.blocksRaycasts = false;
    }

    public void ShowPauseControlsMenu()
    {
        pauseControlsMenu.alpha = 1f;
        pauseControlsMenu.interactable = true;
        pauseControlsMenu.blocksRaycasts = true;
    }

    public void HidePauseControlsMenu()
    {
        pauseControlsMenu.alpha = 0f;
        pauseControlsMenu.interactable = false;
        pauseControlsMenu.blocksRaycasts = false;
    }

    public void ShowRestartMenu()
    {
        restartMenu.alpha = 1f;
        restartMenu.interactable = true;
        restartMenu.blocksRaycasts = true;
    }

    public void HideRestartMenu()
    {
        restartMenu.alpha = 0f;
        restartMenu.interactable = false;
        restartMenu.blocksRaycasts = false;
    }
}
