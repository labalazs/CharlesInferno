using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Start()
    {
        float elapsed = GameData.elapsed;
        timeText.text = $"Time: {(int)(elapsed / 60):00}:{(elapsed % 60):00.0}";
        HighscoreManager.Add(elapsed);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
