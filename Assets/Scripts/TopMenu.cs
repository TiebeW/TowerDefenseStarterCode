using UnityEngine;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    public Text creditsLabel;
    public Text healthLabel;
    public Text waveLabel;

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
}
