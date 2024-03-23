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
        GameManager.instance.Build(PathEnum.Towers.Archer, PathEnum.SiteLevel.level1);
    }

    private void OnSwordButtonClicked()
    {
        GameManager.instance.Build(PathEnum.Towers.Sword, PathEnum.SiteLevel.level1);
    }

    private void OnWizardButtonClicked()
    {
        GameManager.instance.Build(PathEnum.Towers.Wizard, PathEnum.SiteLevel.level1);
    }

    private void OnUpdateButtonClicked()
    {
        if (selectedSite == null)
            return;

        //check if the selected site has a tower type
        if(selectedSite.TowerType == null)
        {
            Debug.LogWarning("Cannot upgrade site because no tower has been built");
            return;
        }

        //increase the level of this selected site by one
        PathEnum.SiteLevel newLevel = selectedSite.Level + 1;

        //Update the site with the new level
        GameManager.instance.Build(selectedSite.TowerType.Value, newLevel);

        //update menu evaluation after upgrading
        EvaluateMenu();
    }

    private void OnDestroyButtonClicked()
    {
        if (selectedSite == null)
            return;

        //destroy the tower on the selected site by setting its level to 0
        selectedSite.SetTower(null, PathEnum.SiteLevel.level0, PathEnum.Towers.None);

        //update menu evaluation after destroying the tower
        EvaluateMenu();
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
