using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wallSetPrefab;

    [SerializeField] private Transform wallsHolder;    

    private float timer = 0f;

    [SerializeField] private float maxTimer;

    private void Update()
    {
        if(timer <= 0f)
        {
            Instantiate(wallSetPrefab, transform.position, Quaternion.identity, wallsHolder);
            timer = maxTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
