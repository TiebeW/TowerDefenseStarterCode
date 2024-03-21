using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archerbutton;
    private Button swordbutton;
    private Button wizardbutton;
    private Button updatebutton;
    private Button destroybutton;

    private VisualElement root;

    private ConstructionSite selectedSite;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerbutton = root.Q<Button>("archerbutton");
        swordbutton = root.Q<Button>("swordbutton");
        wizardbutton = root.Q<Button>("wizardbutton");
        updatebutton = root.Q<Button>("buttonupgrade");
        destroybutton = root.Q<Button>("buttondestroy");

        if (archerbutton != null)
        {
            archerbutton.clicked += OnArcherButtonClicked;
        }

        if (swordbutton != null)
        {
            swordbutton.clicked += OnSwordButtonClicked;
        }

        if (wizardbutton != null)
        {
            wizardbutton.clicked += OnWizardButtonClicked;
        }

        if (updatebutton != null)
        {
            updatebutton.clicked += OnUpdateButtonClicked;
        }

        if (destroybutton != null)
        {
            destroybutton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    private void OnArcherButtonClicked()
    {
        // Implement functionality for archerButton click
    }

    private void OnSwordButtonClicked()
    {
        // Implement functionality for swordButton click
    }

    private void OnWizardButtonClicked()
    {
        // Implement functionality for wizardButton click
    }

    private void OnUpdateButtonClicked()
    {
        // Implement functionality for updateButton click
    }

    private void OnDestroyButtonClicked()
    {
        // Implement functionality for destroyButton click
    }

    private void OnDestroy()
    {
        if (archerbutton != null)
        {
            archerbutton.clicked -= OnArcherButtonClicked;
        }

        if (swordbutton != null)
        {
            swordbutton.clicked -= OnSwordButtonClicked;
        }

        if (wizardbutton != null)
        {
            wizardbutton.clicked -= OnWizardButtonClicked;
        }

        if (updatebutton != null)
        {
            updatebutton.clicked -= OnUpdateButtonClicked;
        }

        if (destroybutton != null)
        {
            destroybutton.clicked -= OnDestroyButtonClicked;
        }
    }

    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;

        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }

        root.visible = true;
        EvaluateMenu(); // Call EvaluateMenu when setting the site
    }

    public void EvaluateMenu()
{
        // Return if selectedSite is null
        if (selectedSite == null)
            return;
        //check site level
        int siteLevel = (int)selectedSite.Level;
    // Enable/disable buttons based on siteLevel
    switch (siteLevel)
    {
        case 0: // If the sitelevel is zero, enable archerButton, wizardButton, and swordButton
            archerbutton.SetEnabled(true);
            swordbutton.SetEnabled(true);
            wizardbutton.SetEnabled(true);
            updatebutton.SetEnabled(false);
            destroybutton.SetEnabled(false);
            break;
        case 1:
        case 2: // If the siteLevel is 1 or 2, enable updateButton and destroyButton
            archerbutton.SetEnabled(false);
            swordbutton.SetEnabled(false);
            wizardbutton.SetEnabled(false);
            updatebutton.SetEnabled(true);
            destroybutton.SetEnabled(true);
            break;
        case 3: // If the siteLevel is 3, only enable destroyButton
            archerbutton.SetEnabled(false);
            swordbutton.SetEnabled(false);
            wizardbutton.SetEnabled(false);
            updatebutton.SetEnabled(false);
            destroybutton.SetEnabled(true);
            break;
        default:
            // Handle any other cases or provide default behavior
            break;
    }
}

}
