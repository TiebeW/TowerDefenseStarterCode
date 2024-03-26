using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    public Text gameResultLabel;
    public Text highScoresLabel;
    public Text[] highScoreEntries; // Array om highscore labels op te slaan
    public Button newGameButton;

    private void Start()
    {
        bool gameWon = GameManager.instance.GetGateHealth() > 0; // Controleer of de speler heeft gewonnen op basis van de Gate Health

        // Afhankelijk van of het spel gewonnen is of niet, pas de tekst aan
        gameResultLabel.text = gameWon ? "Gewonnen!" : "Verloren!";

        // Hier kun je de highscores ophalen uit de GameManager en deze in de labels plaatsen
        DisplayHighScores();

        // Voeg een callback toe aan de nieuwe spelknop
        newGameButton.onClick.AddListener(StartNewGame);
    }

    // Functie om een nieuw spel te starten en terug te gaan naar de GameScene
    private void StartNewGame()
    {
        GameManager.instance.StartGame(); // Start een nieuw spel
        SceneManager.LoadScene("GameScene"); // Laad de GameScene
    }

    // Functie om highscores weer te geven in de labels
    private void DisplayHighScores()
    {
        // Hier kun je highscores ophalen uit de GameManager en deze in de labels plaatsen
        // Voor nu zullen we dummy-gegevens invoegen
        for (int i = 0; i < highScoreEntries.Length; i++)
        {
            highScoreEntries[i].text = "Score " + (i + 1) + ": 1000"; // Voorbeeldscore
        }
    }

    // Inside HighScoreMenu class
    private void UpdateLabels()
    {
        // Get gate health from GameManager and set the health label in TopMenu
        int gateHealth = GameManager.instance.GetGateHealth();
        TopMenu.SetHealthLabel("Gate Health: " + gateHealth);
    }

}
