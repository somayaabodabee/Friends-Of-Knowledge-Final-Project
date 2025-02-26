using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timer = 120f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public GameObject collectionPanel; // The panel to show the message
    public TextMeshProUGUI collectionText;  // The text inside the panel
    public Image[] scoreImages;

    public bool gameEnded = false;

    private float collectionMessageDuration = 1f; // Duration for showing the message
    private float collectionMessageTimer = 0f;   // Timer for displaying the message

    private int lastScore = 0; // To track the last score and avoid showing message if not collected new score

    void Start()
    {
        UpdateUI();
        collectionPanel.SetActive(false); // Hide the panel at the start
    }

    void Update()
    {
        if (!gameEnded)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = "Time: " + Mathf.Ceil(timer).ToString();
            }
            else
            {
                timer = 0;
            }

            UpdateUI();
        }

        // Hide the collection message after the specified duration
        if (collectionMessageTimer > 0)
        {
            collectionMessageTimer -= Time.deltaTime;
            if (collectionMessageTimer <= 0)
            {
                collectionPanel.SetActive(false); // Hide the panel after the message duration
            }
        }
    }

    void UpdateUI()
    {
        int currentScore = GetCurrentScore();
        scoreText.text = "Score: " + currentScore.ToString() + "/" + scoreImages.Length;

        // Only show the message if the score has increased (i.e., new score is collected)
        if (currentScore > lastScore)
        {
            lastScore = currentScore; // Update the last score to the current score
            DisplayCollectionMessage("Good Job!"); // Show the "Good Job!" message
        }
    }

    public int GetCurrentScore()
    {
        int count = 0;
        foreach (Image img in scoreImages)
        {
            if (img.gameObject.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    private void DisplayCollectionMessage(string message)
    {
        collectionText.text = message;
        collectionPanel.SetActive(true); // Show the panel with the message
        collectionMessageTimer = collectionMessageDuration; // Reset the timer for how long the message will stay
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
