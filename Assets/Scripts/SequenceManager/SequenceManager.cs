using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

    public List<SequenceItem> Sequence { get => sequence; set => sequence = value; }

    public int sequenceMaxSize = 2;


    public void Awake()
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

    public void AddToSequence(SequenceItem item)
    {
        if(sequence.Count == sequenceMaxSize)
        {
            return;
        }

        sequence.Add(item);
    }

    public void ResetSequence()
    {
        sequence.Clear();
    }

    public bool CompareSequences(List<SequenceItem> other)
    {
        return sequence.SequenceEqual(other);
    }
}
