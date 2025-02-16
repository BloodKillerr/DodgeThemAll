using UnityEngine;
using UnityEngine.Events;

public class PointSystem : MonoBehaviour
{
    private int points = 0;

    private int maxPoints = 0;

    private int difficultyThreshold = 10;

    private int difficultyLevel = 1;

    public UnityEvent PointSystemUpdateEvent;

    public static PointSystem Instance { get; private set; }
    public int Points { get => points; set => points = value; }
    public int MaxPoints { get => maxPoints; set => maxPoints = value; }

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

    public void IncreasePoints()
    {
        points++;
        PointSystemUpdateEvent.Invoke();
        CheckDifficultyIncrease();
    }

    public void SaveMaxPoints()
    {
        maxPoints = (maxPoints < points) ? points : maxPoints;
        PointSystemUpdateEvent.Invoke();
    }

    private void CheckDifficultyIncrease()
    {
        if(points == difficultyThreshold)
        {
            Debug.Log("Increasing Difficulty!");
            difficultyThreshold = (int)(10 * Mathf.Pow(difficultyLevel, 1.2f) + 10);
            difficultyLevel++;
            SequenceManager.Instance.UpdateGameDifficulty();
        }
    }
}
