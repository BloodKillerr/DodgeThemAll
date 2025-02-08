using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerPositionState
    {
        Center,
        Left,
        Right
    };

    private Rigidbody rb;
    private PlayerPositionState state = PlayerPositionState.Center;

    [SerializeField] private float moveOffset = 10f;
    public float MoveOffset { get => moveOffset; set => moveOffset = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    
    }

    public void MovementEvent(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            float direction = context.ReadValue<float>();
            MovementCheck(direction);
        }
    }

    private void MovementCheck(float direction)
    {
        Transform transform = rb.gameObject.transform;

        switch(state)
        {
            case PlayerPositionState.Left:
                if(direction < 0)
                {
                    return;
                }
                state = PlayerPositionState.Center;
                break;
            case PlayerPositionState.Right:
                if(direction > 0)
                {
                    return;
                }
                state = PlayerPositionState.Center;
                break;
            case PlayerPositionState.Center:
                if (direction > 0)
                {
                    state = PlayerPositionState.Right;
                }
                else
                {
                    state = PlayerPositionState.Left;
                }
                break;
        }

        transform.position += new Vector3(moveOffset * direction, 0, 0);
    }
}
