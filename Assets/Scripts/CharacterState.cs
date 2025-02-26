using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterState : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    public float power = 10f;
    private int killScore = 200;
    public float currentHealth { get; private set; }

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public AudioSource gameOverAudio;
    [SerializeField]
    private AudioClip gameOverSound;

    private void Start()
    {
        currentHealth = maxHealth;
        gameOverPanel.SetActive(false);

        if (gameOverAudio == null)
        {
            gameOverAudio = GetComponent<AudioSource>();
        }
    }

    public void ChangeHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth + " / " + maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (transform.CompareTag("Enemy"))
        {
            UpdateEnemyHealthBar();
        }
        else if (transform.CompareTag("Player"))
        {
            UpdatePlayerHealthBar();
        }
    }

    private void UpdateEnemyHealthBar()
    {
        Image healthBar = transform.Find("Canvas")?.GetChild(1)?.GetComponent<Image>();
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("Health bar not found for Enemy.");
        }
    }

    private void UpdatePlayerHealthBar()
    {
        Image panelHealth = LevelManager.instance?.MainCan?.Find("PanelStats")?.Find("Panel Health")?.GetComponent<Image>();
        TextMeshProUGUI textHealth = LevelManager.instance?.MainCan?.Find("PanelStats")?.Find("TextHealth")?.GetComponent<TextMeshProUGUI>();

        if (panelHealth != null)
        {
            panelHealth.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("Panel Health not found for Player.");
        }

        if (textHealth != null)
        {
            textHealth.text = Mathf.RoundToInt((currentHealth / maxHealth) * 100) + " %";
        }
        else
        {
            Debug.LogWarning("Text Health not found for Player.");
        }
    }

    private void Die()
    {
        if (transform.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            gameOverText.text = "Game Over";
            gameOverPanel.SetActive(true);
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(RestartGame);

            if (gameOverAudio != null && gameOverSound != null)
            {
                gameOverAudio.PlayOneShot(gameOverSound);
            }

            Destroy(gameObject);
        }
        else if (transform.CompareTag("Enemy"))
        {
            LevelManager.instance.score += killScore;
            Destroy(gameObject);

            if (LevelManager.instance.particals.Length > 2)
            {
                Instantiate(LevelManager.instance.particals[2], transform.position, transform.rotation);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);  // Always load Scene 0 (first scene in the build)
    }
}
