using System;
using UnityEngine;

public static class HighscoreManager
{
    private const string key = "Highscores";
    private const int maxEntries = 10;

    public static HighscoreData Load()
    {
        string json = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrWhiteSpace(json))
        {
            return new HighscoreData();
        }
        return JsonUtility.FromJson<HighscoreData>(json);
    }

    public static void Add(float time)
    {
        HighscoreData data = Load();
        data.entries.Add(new HighscoreEntry()
        {
            time = time,
            date = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
        });
        data.entries.Sort((a, b) => a.time.CompareTo(b.time));
        if (data.entries.Count > maxEntries)
        {
            data.entries.RemoveAt(data.entries.Count - 1);
        }
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteKey(key);
    }
}
