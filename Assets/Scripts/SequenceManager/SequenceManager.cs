using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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

    private List<SequenceItem> sequence = new List<SequenceItem>();

    private List<GameObject> sequenceItemIconObjects = new List<GameObject>();

    [SerializeField] private GameObject sequenceItemPrefab = null;

    [SerializeField] private Transform sequenceItemsPlaceholder;

    public List<SequenceItem> Sequence { get => sequence; set => sequence = value; }

    public int sequenceMaxSize = 2;


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
        UIManager.Instance.IconsUpdateEvent.AddListener(UpdateAllIcons);
    }

    public void AddToSequence(SequenceItem item)
    {
        if(sequence.Count == sequenceMaxSize)
        {
            return;
        }

        sequence.Add(item);
        GameObject go = Instantiate(sequenceItemPrefab, sequenceItemsPlaceholder);
        go.GetComponent<Image>().sprite = UIManager.Instance.GetCorrectIcon(item);
        sequenceItemIconObjects.Add(go);
    }

    public void ResetSequence()
    {
        foreach (Transform child in sequenceItemsPlaceholder)
        {
            Destroy(child.gameObject);
        }

        sequenceItemIconObjects.Clear();
        sequence.Clear();
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
}
