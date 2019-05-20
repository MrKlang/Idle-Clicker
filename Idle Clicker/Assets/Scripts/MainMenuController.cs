using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI HighScoreText;
    [SerializeField]
    private Button NewGameButton;
    [SerializeField]
    private Button ExitGameButton;

    void Start()
    {
        DisplayBestTimeFromPlayerPrefs();
        NewGameButton.onClick.AddListener(() => { StartNewGame(); });
        ExitGameButton.onClick.AddListener(() => { ExitGame(); });
    }

    public void DisplayBestTimeFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            var bestTime = PlayerPrefs.GetFloat("BestTime");
            TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);
            HighScoreText.text = string.Format("Best time: {0:0} hrs {1:00} mins {2:00} secs",timeSpan.Hours,timeSpan.Minutes,timeSpan.Seconds);
        }
        else
        {
            HighScoreText.text = "Best time: NONE!";
        }
    }

    private void ExitGame() =>
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif


    private void StartNewGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
}
