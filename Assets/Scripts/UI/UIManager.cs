using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<IconDictionaryEntry> keyboardIcons;
    [SerializeField] private List<IconDictionaryEntry> xBoxIcons;
    [SerializeField] private List<IconDictionaryEntry> psIcons;

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

    public Sprite GetCorrectIcon(SequenceManager.SequenceItem item)
    {
        switch(InputManager.Instance.LastUsedDevice)
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
}
