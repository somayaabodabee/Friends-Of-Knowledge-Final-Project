using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject exitPopup;
    public Button[] menuButtons;
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenu";

    private int currentSelection = 0;

    private void Start()
    {
        mainMenu.SetActive(true);
        exitPopup.SetActive(false);
        if (menuButtons.Length > 0)
            menuButtons[currentSelection].Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateMenu(-1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateMenu(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NavigateMenu(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NavigateMenu(1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectButton();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopup.SetActive(true);
        }
    }

    private void NavigateMenu(int direction)
    {
        currentSelection = Mathf.Clamp(currentSelection + direction, 0, menuButtons.Length - 1);
        menuButtons[currentSelection].Select();
    }

    private void SelectButton()
    {
        menuButtons[currentSelection].onClick.Invoke();
    }

    public void PlayButtonClicked()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitButtonClicked()
    {
        exitPopup.SetActive(true);
    }

    public void ConfirmExit()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void CancelExit()
    {
        exitPopup.SetActive(false);
    }
}
