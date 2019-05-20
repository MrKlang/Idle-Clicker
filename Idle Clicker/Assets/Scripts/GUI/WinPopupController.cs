using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopupController : MonoBehaviour
{
    public TextMeshProUGUI WinText;
    public Button ExitToMenuButton;

    private void Start()
    {
        ExitToMenuButton.onClick.AddListener(()=> 
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
        });
    }

    private void SetWinText(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        WinText.text = string.Format("Congratulations!\nYou beat the game in:\n{0:0} hrs {1:00} mins {2:00} secs", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    public void ShowWinPopup(float seconds)
    {
        Time.timeScale = 0;
        SetWinText(seconds);
        gameObject.SetActive(true);
    }
}
