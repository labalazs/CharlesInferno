using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Highscores()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
