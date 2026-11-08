using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreMenuManager : MonoBehaviour
{
    public TextMeshProUGUI listDisplay;
    private HighscoreData data = new HighscoreData();

    private void Start()
    {
        data = HighscoreManager.Load();
        LoadText();
    }

    public void Clear()
    {
        data = new HighscoreData();
        HighscoreManager.Clear();
        LoadText();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadText()
    {
        listDisplay.text = GetText();
    }

    private string GetText()
    {
        if (data == null || data.entries == null || data.entries.Count == 0)
        {
            return "There are no highscores set yet...";
        }
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.entries.Count; i++)
        {
            string line = $"{i + 1}: {data.entries[i].date}, <b>{(int)(data.entries[i].time / 60):00}:{(data.entries[i].time % 60):00}</b>\n";
            sb.Append(line);
        }
        return sb.ToString();
    }
}
