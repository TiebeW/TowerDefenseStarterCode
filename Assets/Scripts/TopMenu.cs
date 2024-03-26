using UnityEngine;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    public Text creditsLabel;
    public Text healthLabel;
    public Text waveLabel;
    public Button waveButton;

    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }

    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    public void WaveButton_clicked()
    {
        // Start the wave
        GameManager.instance.StartWave();

        // Disable the wave button
        waveButton.interactable = false;
    }

    public void EnableWaveButton()
    {
        // Enable the wave button
        waveButton.interactable = true;
    }
}
