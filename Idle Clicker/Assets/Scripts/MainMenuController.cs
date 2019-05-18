using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        NewGameButton.onClick.AddListener(() => { StartNewGame(); });
        ExitGameButton.onClick.AddListener(() => { ExitGame(); });
    }

    private void ExitGame() =>
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif


    private void StartNewGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
}
