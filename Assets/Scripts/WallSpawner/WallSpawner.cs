using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallSetPrefab;

    [SerializeField] private Transform wallsHolder;    

    private float timer = 0f;

    [SerializeField] private float maxTimer;

    public static WallSpawner Instance { get; private set; }

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

    private void Update()
    {
        if(timer <= 0f && !GameManager.Instance.IsPaused && GameManager.Instance.IsPlaying)
        {
            Instantiate(wallSetPrefab, transform.position, Quaternion.identity, wallsHolder);
            timer = maxTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void ResetWalls()
    {
        foreach (Transform child in wallsHolder)
        {
            Destroy(child.gameObject);
        }

        timer = 0f;
    }
}
