using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeSceneOnBoard : MonoBehaviour
{
    public string nextSceneName;
    private bool isNearBoard = false;

    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    void Start()
    {
        messagePanel.SetActive(false);
    }

    void Update()
    {
        if (isNearBoard)
        {
            messagePanel.SetActive(true);
            messageText.text = "Press E to proceed to the next level and solve puzzles.";

            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadNextScene();
            }
        }
        else
        {
            messagePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBoard = false;
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
