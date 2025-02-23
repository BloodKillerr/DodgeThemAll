using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private string lastUsedDevice = "Keyboard";

    public string LastUsedDevice { get => lastUsedDevice; set => lastUsedDevice = value; }

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

    void Start()
    {

    }

    public void UpdateLastUsedDevice(PlayerInput playerInput)
    {
        switch(playerInput.currentControlScheme)
        {
            case "KeyBoardMouse":
                lastUsedDevice = "Keyboard";
                break;
            case "XBoxController":
                lastUsedDevice = "XBox";
                break;
            case "PSController":
                lastUsedDevice = "PlayStation";
                break;
        }

        if(UIManager.Instance != null)
        {
            UIManager.Instance.IconsUpdateEvent.Invoke();
        }
    }
}
