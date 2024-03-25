using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    // UI Elements
    private Button archerbutton;
    private Button swordbutton;
    private Button wizardbutton;
    private Button updatebutton;
    private Button destroybutton;
    private VisualElement root;

    // Selected construction site
    private ConstructionSite selectedSite;

    void Start()
    {
        // Get the root visual element
        root = GetComponent<UIDocument>().rootVisualElement;

        // Assign buttons
        archerbutton = root.Q<Button>("archerbutton");
        swordbutton = root.Q<Button>("swordbutton");
        wizardbutton = root.Q<Button>("wizardbutton");
        updatebutton = root.Q<Button>("buttonupgrade");
        destroybutton = root.Q<Button>("buttondestroy");

        // Add button click event listeners
        archerbutton.clicked += OnArcherButtonClicked;
        swordbutton.clicked += OnSwordButtonClicked;
        wizardbutton.clicked += OnWizardButtonClicked;
        updatebutton.clicked += OnUpdateButtonClicked;
        destroybutton.clicked += OnDestroyButtonClicked;

        // Hide the menu initially
        root.visible = false;
    }

    // Event handlers for button clicks
    private void OnArcherButtonClicked() { GameManager.instance.Build(PathEnum.Towers.Archer, PathEnum.SiteLevel.level1); }
    private void OnSwordButtonClicked() { GameManager.instance.Build(PathEnum.Towers.Sword, PathEnum.SiteLevel.level1); }
    private void OnWizardButtonClicked() { GameManager.instance.Build(PathEnum.Towers.Wizard, PathEnum.SiteLevel.level1); }
    private void OnUpdateButtonClicked() { /* Logic for upgrading towers */ }
    private void OnDestroyButtonClicked() { /* Logic for destroying towers */ }

    private void OnDestroy()
    {
        // Remove button click event listeners
        archerbutton.clicked -= OnArcherButtonClicked;
        swordbutton.clicked -= OnSwordButtonClicked;
        wizardbutton.clicked -= OnWizardButtonClicked;
        updatebutton.clicked -= OnUpdateButtonClicked;
        destroybutton.clicked -= OnDestroyButtonClicked;
    }

    // Method to set the selected construction site
    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;
        // Show/hide the menu based on whether a site is selected
        root.visible = selectedSite != null;
        // Evaluate the menu based on the selected site
        EvaluateMenu();
    }

    // Method to evaluate and update the menu buttons based on the selected site
    public void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        // Get the level of the selected site
        int siteLevel = (int)selectedSite.Level;

        // Get the available credits
        int availableCredits = GameManager.instance.GetCredits();

        // Enable/disable buttons based on site level and available credits
        switch (siteLevel)
        {
            case 0:
                // If the site level is zero, enable building buttons and disable update/destroy buttons
                archerbutton.SetEnabled(availableCredits >= GameManager.instance.GetCost(PathEnum.Towers.Archer, PathEnum.SiteLevel.level0));
                swordbutton.SetEnabled(availableCredits >= GameManager.instance.GetCost(PathEnum.Towers.Sword, PathEnum.SiteLevel.level0));
                wizardbutton.SetEnabled(availableCredits >= GameManager.instance.GetCost(PathEnum.Towers.Wizard, PathEnum.SiteLevel.level0));
                updatebutton.SetEnabled(false);
                destroybutton.SetEnabled(false);
                break;
            case 1:
            case 2:
                // If the site level is 1 or 2, enable update and destroy buttons and disable building buttons
                archerbutton.SetEnabled(false);
                swordbutton.SetEnabled(false);
                wizardbutton.SetEnabled(false);
                updatebutton.SetEnabled(availableCredits >= GameManager.instance.GetCost((PathEnum.Towers)selectedSite.TowerType, selectedSite.Level + 1));
                destroybutton.SetEnabled(true);
                break;
            case 3:
                // If the site level is 3, enable only the destroy button
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
