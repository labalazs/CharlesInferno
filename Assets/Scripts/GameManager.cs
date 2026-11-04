using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private InputAction pauseAction;
    private float elapsed = 0;
    private bool isRunning = false;

    private void Start()
    {
        elapsed = 0;
        isRunning = true;
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsed += Time.deltaTime;
            if (pauseAction.ReadValue<float>() != 0)
            {
                Pause();
            }
        }
    }

    public void Win()
    {
        isRunning = false;
        GameData.elapsed = elapsed;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LoadScene("Victory");
    }

    private void Pause()
    {
        isRunning = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        isRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void Retry()
    {
        gameOverMenu.SetActive(false);
    }

    public void Exit()
    {
        LoadScene("MainMenu");
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
