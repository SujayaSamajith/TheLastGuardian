using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI victoryKillsText;
    [SerializeField] private TextMeshProUGUI victoryTimeText;
    private bool gameFinished;

    private int killCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    private void Update()
    {
        timerText.text = Time.time.ToString("F2") + "s";
    }

    public void EnableGameOverUI()
    {
        if (gameFinished)
            return;

        Time.timeScale = .5f;
        gameOverUI.SetActive(true);
    }

    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
        
    }

    public void EnableVictoryUI()
    {
        if (gameFinished)
            return;

        gameFinished = true;

        if (victoryUI != null)
            victoryUI.SetActive(true);

        if (victoryKillsText != null)
            victoryKillsText.text = "Kills: " + killCount;

        if (victoryTimeText != null)
            victoryTimeText.text = "Time: " + Time.time.ToString("F2") + "s";


        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}