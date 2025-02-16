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
}
