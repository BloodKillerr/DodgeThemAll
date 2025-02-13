using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    private List<SequenceManager.SequenceItem> sequence = new List<SequenceManager.SequenceItem>();

    private List<GameObject> sequenceItemIconObjects = new List<GameObject>();

    [SerializeField] private GameObject sequenceItemPrefab = null;

    [SerializeField] private Transform sequenceItemsPlaceholder;

    private void Start()
    {
        sequence = GenerateSequence(SequenceManager.Instance.sequenceMaxSize);
        UIManager.Instance.IconsUpdateEvent.AddListener(UpdateAllIcons);
        CreateSequenceUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            if (SequenceManager.Instance.CompareSequences(sequence))
            {
                Debug.Log("Correct Sequence!");
                SequenceManager.Instance.ResetSequence();
                Destroy(gameObject.transform.parent.gameObject);
                PointSystem.Instance.IncreasePoints();
            }
            else
            {
                Debug.Log("Wrong Sequence!");
                Destroy(other.gameObject);
                PointSystem.Instance.SaveMaxPoints();
            }
        }
        else
        {
            Debug.Log("Player not Detected");
        }
    }

    private List<SequenceManager.SequenceItem> GenerateSequence(int size)
    {
        List<SequenceManager.SequenceItem> enumList = new List<SequenceManager.SequenceItem>();
        Array values = Enum.GetValues(typeof(SequenceManager.SequenceItem));

        for (int i = 0; i < size; i++)
        {
            SequenceManager.SequenceItem randomEnum = (SequenceManager.SequenceItem)values.GetValue(UnityEngine.Random.Range(0, values.Length));
            enumList.Add(randomEnum);
        }

        return enumList;
    }

    private void CreateSequenceUI()
    {
        foreach (Transform child in sequenceItemsPlaceholder)
        {
            Destroy(child.gameObject);
        }

        foreach (SequenceManager.SequenceItem item in sequence)
        {
            GameObject go = Instantiate(sequenceItemPrefab, sequenceItemsPlaceholder);
            go.GetComponent<Image>().sprite = UIManager.Instance.GetCorrectIcon(item);
            sequenceItemIconObjects.Add(go);
        }
    }

    private void UpdateAllIcons()
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            sequenceItemIconObjects[i].GetComponent<Image>().sprite = UIManager.Instance.GetCorrectIcon(sequence[i]);
        }
    }
}
