using UnityEngine;
using UnityEngine.Events;

public class PointSystem : MonoBehaviour
{
    private int points = 0;

    private int maxPoints = 0;

    private int difficultyThreshold = 10;

    private int difficultyLevel = 1;

    private string maxPointsKey = "maxPoints";

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

    private void Start()
    {
        if (PlayerPrefs.HasKey(maxPointsKey))
        {
            maxPoints = PlayerPrefs.GetInt(maxPointsKey);
            PointSystemUpdateEvent.Invoke();
        }
        else
        {
            Debug.Log("The highscore is 0");
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
        if(maxPoints < points)
        {
            maxPoints = points;
            PlayerPrefs.SetInt(maxPointsKey, maxPoints);
        }
        PointSystemUpdateEvent.Invoke();
    }

    public void ResetMaxPoints()
    {
        PlayerPrefs.DeleteKey(maxPointsKey);
        maxPoints = 0;
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

    public void ResetPointsSystem()
    {
        points = 0;
        difficultyThreshold = 10;
        difficultyLevel = 1;
        PointSystemUpdateEvent.Invoke();
    }
}
