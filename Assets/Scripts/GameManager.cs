using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float elapsed = 0;
    private bool isRunning = false;

    private void Start()
    {
        elapsed = 0;
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsed += Time.deltaTime;
        }
    }

    public void Win()
    {
        isRunning = false;
        GameData.elapsed = elapsed;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Victory");
    }
}
