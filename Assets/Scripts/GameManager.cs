using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject lava;
    private InputAction pauseAction;
    private float elapsed = 0;
    private bool isRunning = false;
    private PlayerMovement playerMovement;
    private LavaRiseController lavaController;

    private void Start()
    {
        elapsed = 0;
        isRunning = true;
        pauseAction = InputSystem.actions.FindAction("Pause");
        playerMovement = player.GetComponent<PlayerMovement>();
        lavaController = lava.GetComponent<LavaRiseController>();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        lavaController.Stop();
        LoadScene("Victory");
    }

    private void Pause()
    {
        isRunning = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void GameOver()
    {
        isRunning = false;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverMenu.SetActive(true);
    }

    public void Continue()
    {
        isRunning = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }

    public void Retry()
    {
        playerMovement.Respawn();
        lavaController.Reset();
        isRunning = true;
        Time.timeScale = 1f;
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
