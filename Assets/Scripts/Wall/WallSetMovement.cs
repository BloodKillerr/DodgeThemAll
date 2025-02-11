using UnityEngine;

public class WallSetMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
