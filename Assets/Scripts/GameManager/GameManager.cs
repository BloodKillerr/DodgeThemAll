using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private GameObject audioText;

    public static GameManager Instance { get; private set; }

    private bool isPaused = true;

    public bool IsPaused { get => isPaused; set => isPaused = value; }

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
        if(PlayerPrefs.HasKey("volume"))
        {
            LoadVolume();
        }
        else
        {
            SetVolume();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIManager.Instance.HideMainMenu();
        audioText.SetActive(false);
        audioSlider.gameObject.SetActive(false);
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
}
