using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;

public class SequenceManager : MonoBehaviour
{
    public enum SequenceItem
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        SPECIAL1,
        SPECIAL2
    };

    public static SequenceManager Instance { get; private set; }

    public UnityEvent DifficultyUpdateEvent;

    private List<SequenceItem> sequence = new List<SequenceItem>();

    private List<GameObject> sequenceItemIconObjects = new List<GameObject>();

    private AudioSpawner audioSpawner;

    [SerializeField] private GameObject sequenceItemPrefab = null;

    public List<SequenceItem> Sequence { get => sequence; set => sequence = value; }

    public int sequenceMaxSize;

    public float wallSetSpeed;


    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        audioSpawner = GetComponent<AudioSpawner>();
        UIManager.Instance.IconsUpdateEvent.AddListener(UpdateAllIcons);
    }

    public void AddToSequence(SequenceItem item)
    {
        if(!GameManager.Instance.IsPlaying || GameManager.Instance.IsPaused)
        {
            return;
        }

        if(sequence.Count == sequenceMaxSize)
        {
            return;
        }

        sequence.Add(item);
        GameObject go = Instantiate(sequenceItemPrefab, Player.Instance.SequenceItemsPlaceholder);
        go.GetComponent<Image>().sprite = UIManager.Instance.GetCorrectIcon(item);
        sequenceItemIconObjects.Add(go);
        audioSpawner.SpawnAudioObject(Player.Instance.gameObject.transform);
    }

    public void ResetSequence()
    {
        if (!GameManager.Instance.IsPlaying || GameManager.Instance.IsPaused)
        {
            return;
        }

        foreach (Transform child in Player.Instance.SequenceItemsPlaceholder)
        {
            Destroy(child.gameObject);
        }

        sequenceItemIconObjects.Clear();
        sequence.Clear();
        audioSpawner.SpawnAudioObject(Player.Instance.gameObject.transform);
    }

    public bool CompareSequences(List<SequenceItem> other)
    {
        return sequence.SequenceEqual(other);
    }

    private void UpdateAllIcons()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            sequenceItemIconObjects[i].GetComponent<Image>().sprite = UIManager.Instance.GetCorrectIcon(sequence[i]);
        }
    }

    public void UpdateGameDifficulty()
    {
        if(sequenceMaxSize < 8)
        {
            sequenceMaxSize++;
        }

        wallSetSpeed += 2;
        DifficultyUpdateEvent.Invoke();
    }

    public void ResetState()
    {
        sequenceItemIconObjects.Clear();
        sequence.Clear();
        sequenceMaxSize = 2;
        wallSetSpeed = 10f;
    }
}
