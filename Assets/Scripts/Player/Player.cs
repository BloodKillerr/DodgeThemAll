using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private SequenceManager sequenceManager;

    [SerializeField] private Transform sequenceItemsPlaceholder;

    public static Player Instance { get; private set; }
    public Transform SequenceItemsPlaceholder { get => sequenceItemsPlaceholder; set => sequenceItemsPlaceholder = value; }
    public PlayerInput PlayerInput { get => playerInput; set => playerInput = value; }

    private PlayerInput playerInput;

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
        playerInput = GetComponent<PlayerInput>();
        playerInput.controlsChangedEvent.AddListener(InputManager.Instance.UpdateLastUsedDevice);
        playerInput.actions["PauseResume"].performed += GameManager.Instance.PauseResumeEvent;
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
