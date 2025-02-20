using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public void StartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIManager.Instance.HideMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
