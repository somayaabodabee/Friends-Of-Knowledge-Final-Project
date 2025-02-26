using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private TextMeshProUGUI codeText;
    private string codeTextValue = "";

    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private TextMeshProUGUI instructionsText;
    [SerializeField] private GameObject collectedNumbersPanel;
    [SerializeField] private TextMeshProUGUI collectedNumbersText;
    [SerializeField] private GameObject completionPanel;
    [SerializeField] private TextMeshProUGUI completionText;

    public string safeCode = "";
    public GameObject codePanel;

    [SerializeField] private List<GameObject> allNumbers = new List<GameObject>();
    private List<GameObject> activeNumbers = new List<GameObject>();
    public List<int> collectedNumbers = new List<int>();

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip keypadOpenSound;
    [SerializeField] private AudioClip collectedNumbersPanelSound;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip levelCompleteSound;

    private bool doorOpened = false;

    void Start()
    {
        HideAllNumbers();
        SelectRandomNumbers();
        collectedNumbersPanel.SetActive(false);
        codePanel.SetActive(false);
        completionPanel.SetActive(false);
        ShowInstructions();
    }

    void Update()
    {
        codeText.text = codeTextValue;

        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha0 + i)) || Input.GetKeyDown((KeyCode)(KeyCode.Keypad0 + i)))
            {
                AddDigit(i.ToString());
            }
        }

        if (codeTextValue.Length == 3 && !doorOpened)
        {
            if (codeTextValue == safeCode)
            {
                PlaySound(doorOpenSound);
                anim.SetTrigger("OpenDoor");
                codePanel.SetActive(false);
                codeTextValue = "";
                doorOpened = true;
            }
            else
            {
                StartCoroutine(ResetCodeAfterDelay());
            }
        }
    }

    public void AddDigit(string digit)
    {
        if (codeTextValue.Length < 4)
        {
            codeTextValue += digit;
        }
    }

    private void HideAllNumbers()
    {
        foreach (GameObject number in allNumbers)
        {
            number.SetActive(false);
        }
    }

    private void SelectRandomNumbers()
    {
        List<GameObject> shuffledNumbers = new List<GameObject>(allNumbers);
        shuffledNumbers = shuffledNumbers.OrderBy(n => Random.value).ToList();

        activeNumbers = shuffledNumbers.Take(3).ToList();

        foreach (GameObject number in activeNumbers)
        {
            number.SetActive(true);
        }
    }

    public void CollectNumber(Numbers number)
    {
        if (!collectedNumbers.Contains(number.Number))
        {
            collectedNumbers.Add(number.Number);
            activeNumbers.Remove(number.gameObject);
            number.gameObject.SetActive(false);
            PlaySound(collectSound);
        }

        if (collectedNumbers.Count >= 3)
        {
            SetSafeCode();
            collectedNumbersPanel.SetActive(true);
            collectedNumbersText.text = "Well done! You did a great job. Now, enter the collected numbers from largest to smallest in the keypad.";
            PlaySound(collectedNumbersPanelSound);
            StartCoroutine(HideCollectedNumbersPanelAfterDelay(2f));
            OpenKeypad();
        }
    }

    private void SetSafeCode()
    {
        collectedNumbers.Sort((a, b) => b.CompareTo(a));
        safeCode = string.Join("", collectedNumbers);
    }

    private void OpenKeypad()
    {
        codePanel.SetActive(true);
        PlaySound(keypadOpenSound);
    }

    private void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
        instructionsText.text = "Collect the numbers from largest to smallest.";
        StartCoroutine(HidePanelsAfterDelay());
    }

    private IEnumerator HidePanelsAfterDelay()
    {
        yield return new WaitForSeconds(6);
        instructionsPanel.SetActive(false);
        collectedNumbersPanel.SetActive(false);
        codePanel.SetActive(false);
        completionPanel.SetActive(false);
    }

    private IEnumerator HideCollectedNumbersPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        collectedNumbersPanel.SetActive(false);
    }

    private IEnumerator ResetCodeAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        codeTextValue = "";
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doorOpened && other.CompareTag("Player"))
        {
            ShowCompletionMessage();
        }
    }

    private void ShowCompletionMessage()
    {
        completionPanel.SetActive(true);
        completionText.text = "Well done! You have successfully completed the first level.\r\nComplete the phrase, then go to the board and press E to proceed to the next level";
        PlaySound(levelCompleteSound);
        StartCoroutine(HidePanelsAfterDelay());
    }
}