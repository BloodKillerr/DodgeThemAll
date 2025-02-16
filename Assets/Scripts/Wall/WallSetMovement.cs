using UnityEngine;

public class WallSetMovement : MonoBehaviour
{
    private float moveSpeed;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Start()
    {
        moveSpeed = SequenceManager.Instance.wallSetSpeed;
        SequenceManager.Instance.DifficultyUpdateEvent.AddListener(UpdateSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        moveSpeed = SequenceManager.Instance.wallSetSpeed;
    }
}
