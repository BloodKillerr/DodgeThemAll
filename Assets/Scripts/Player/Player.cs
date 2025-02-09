using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private SequenceManager sequenceManager;

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
        sequenceManager = SequenceManager.Instance;
    }

    public void ArrowsEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();

            if(direction.y > 0)
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.UP);
            }
            else if(direction.y < 0)
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.DOWN);
            }
            else if(direction.x > 0)
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.RIGHT);
            }
            else if(direction.x < 0)
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.LEFT);
            }
        }
    }

    public void SpecialEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float direction = context.ReadValue<float>();

            if(direction > 0)
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.SPECIAL2);
            }
            else
            {
                sequenceManager.AddToSequence(SequenceManager.SequenceItem.SPECIAL1);
            }
        }
    }

    public void ResetEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sequenceManager.ResetSequence();
        }
    }
}
