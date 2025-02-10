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

    public void Awake()
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
}
