using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public InputField textField;

    private void Start()
    {
        // Schakel de Start Button uit wanneer het menu opent
        startButton.interactable = false;

        // Voeg callbacks toe aan de knoppen
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);

       
    }

    private void OnDestroy()
    {
        // Verwijder de callbacks om geheugenlekken te voorkomen
        startButton.onClick.RemoveListener(StartGame);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        // Laad de GameScene
        SceneManager.LoadScene("GameScene");
    }

    private void QuitGame()
    {
        // Sluit het spel af
        Application.Quit();
    }
}
