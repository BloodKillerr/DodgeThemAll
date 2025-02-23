using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private GameObject audioText;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform objectsHolder;

    private int targetFrameRate = 120;

    public static GameManager Instance { get; private set; }

    private bool isPaused = false;

    private bool isPlaying = false;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

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

        Time.timeScale = 0f;
    }

    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        if (PlayerPrefs.HasKey("volume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }

    public void PauseResumeEvent(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(isPaused && isPlaying)
            {
                isPaused = false;
                ResumeGame();
            }
            else if(!isPaused && isPlaying)
            {
                isPaused = true;
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isPlaying = true;
        UIManager.Instance.HideMainMenu();
        audioText.SetActive(false);
        audioSlider.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        UIManager.Instance.ShowPauseMenu();
        audioText.SetActive(true);
        audioSlider.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIManager.Instance.HidePauseControlsMenu();
        UIManager.Instance.HidePauseMenu();
        audioText.SetActive(false);
        audioSlider.gameObject.SetActive(false);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        isPlaying = false;
        UIManager.Instance.ShowRestartMenu();
        audioText.SetActive(true);
        audioSlider.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPlaying = true;
        UIManager.Instance.HideRestartMenu();
        audioText.SetActive(false);
        audioSlider.gameObject.SetActive(false);
        ResetGameState();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        float volume = audioSlider.value;
        audioMixer.SetFloat("main", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void LoadVolume()
    {
        audioSlider.value = PlayerPrefs.GetFloat("volume");
        SetVolume();
    }

    private void ResetGameState()
    {
        Instantiate(playerPrefab, new Vector3(-2.5f, 0, 5), Quaternion.identity, objectsHolder);
        SequenceManager.Instance.ResetState();
        WallSpawner.Instance.ResetWalls();
        PointSystem.Instance.ResetPointsSystem();
    }
}
