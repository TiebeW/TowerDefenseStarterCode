using UnityEngine;
using UnityEngine.UIElements;

public class NewTopMenu : MonoBehaviour
{
    public Label waveLabel;
    public Label creditsLabel;
    public Label healthLabel;
    public Button startWaveButton;

    private int currentWave = 1;
    private int playerCredits = 100;
    private int gateHealth = 100;

    void Start()
    {
        // Zoek de button en voeg een click event listener toe
        startWaveButton.clicked += StartWave;

        // Update de labels
        UpdateLabels();
    }

    void OnDestroy()
    {
        // Verwijder de click event listener om memory leaks te voorkomen
        startWaveButton.clicked -= StartWave;
    }

    void UpdateLabels()
    {
        // Update de tekst van de labels
        waveLabel.text = "Wave: " + currentWave.ToString();
        creditsLabel.text = "Credits: " + playerCredits.ToString();
        healthLabel.text = "Gate Health: " + gateHealth.ToString();
    }

    void StartWave()
    {
        // Verminder credits bij het starten van een nieuwe wave
        int waveCost = 10; // bijvoorbeeld
        if (playerCredits >= waveCost)
        {
            playerCredits -= waveCost; // Verminder credits
            currentWave++; // Verhoog de huidige wave

            // Update de labels om de veranderingen weer te geven
            UpdateLabels();

            // Hier kun je verdere logica toevoegen om de wave daadwerkelijk te starten
            Debug.Log("Wave " + currentWave.ToString() + " is gestart!");
        }
        else
        {
            Debug.Log("Je hebt niet genoeg credits om een nieuwe wave te starten!");
        }
    }

}