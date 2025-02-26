using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossHandler : MonoBehaviour
{
    public GameController gameController;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public string nextSceneName; // Set this in the Inspector

    void Start()
    {
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameController.gameEnded)
        {
            if (gameController.timer > 0)
            {
                if (gameController.GetCurrentScore() >= gameController.scoreImages.Length)
                {
                    WinGame();
                }
            }
            else
            {
                GameOver();
            }
        }
    }

    void WinGame()
    {
        gameController.gameEnded = true;
        winPanel.SetActive(true);
    }

    void GameOver()
    {
        gameController.gameEnded = true;
        gameOverPanel.SetActive(true);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

