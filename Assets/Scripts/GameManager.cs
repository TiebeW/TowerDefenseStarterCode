using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    private ConstructionSite selectedSite;

    private int credits;
    private int health;
    private int currentWave;

    public TopMenu topMenu;

    private bool waveActive = false;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        // Initialize values
        credits = 200;
        health = 10;
        currentWave = 0;

        // Ensure waveActive is set to false
        waveActive = false;

        // Update the labels in the TopMenu
        UpdateLabels();
    }

    public void StartWave()
    {
        // Verhoog de waarde van currentWave
        currentWave++;

        // Verander het label voor de huidige golf in topMenu
        topMenu.SetWaveLabel("Wave: " + currentWave);

        // Verander waveActive naar true
        waveActive = true;

        // Voeg hier verdere logica toe voor het starten van de golf, zoals het spawnen van vijanden, etc.
    }

    public void EndWave()
    {
        // Verander waveActive naar false
        waveActive = false;

        // Voeg hier verdere logica toe voor het einde van de golf, zoals het stoppen van vijanden, etc.
    }


    void UpdateLabels()
    {
        // Set the text for each label in the TopMenu
        topMenu.SetCreditsLabel("Credits: " + credits);
        topMenu.SetHealthLabel("Gate Health: " + health);
        topMenu.SetWaveLabel("Wave: " + currentWave);
    }

    public void AttackGate()
    {
        // Reduce health by 1
        health--;

        // Update the health label in TopMenu
        topMenu.SetHealthLabel("Gate Health: " + health);
    }

    public void AddCredits(int amount)
    {
        // Increase credits by the given amount
        credits += amount;

        // Update the credits label in TopMenu
        topMenu.SetCreditsLabel("Credits: " + credits);

        // Evaluate the tower menu (to be implemented)
    }

    public void RemoveCredits(int amount)
    {
        // Decrease credits by the given amount
        credits -= amount;

        // Update the credits label in TopMenu
        topMenu.SetCreditsLabel("Credits: " + credits);

        // Evaluate the tower menu (to be implemented)
    }

    public int GetCredits()
    {
        // Return the current credits
        return credits;
    }

    public int GetCost(PathEnum.Towers type, PathEnum.SiteLevel level, bool selling = false)
    {
        int cost = 0;
        // Calculate the cost based on tower type, site level, and selling status
        // This implementation should be according to your game's logic

        return cost;
    }

    // Referentie naar het menu
    public GameObject menu;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methode om het menu te openen wanneer een site is geselecteerd
    public void OpenMenu()
    {
        menu.SetActive(true); // Stel in dat het menu actief is
        // Hier kun je verdere logica toevoegen om het menu aan te passen of te vullen op basis van de geselecteerde site
    }

    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
        // Open het menu wanneer een site is geselecteerd
        //OpenMenu();

        // Hier verkrijg je een referentie naar TowerMenu via GetComponent
        TowerMenu towerMenu = menu.GetComponent<TowerMenu>();
        if (towerMenu != null)
        {
            towerMenu.SetSite(selectedSite);
        }
    }

    public void Build(PathEnum.Towers type, PathEnum.SiteLevel level)
    {
        // Check if a site is selected
        if (selectedSite == null)
        {
            Debug.LogWarning("No site selected to build on.");
            return;
        }

        // Select the appropriate list of tower prefabs based on the tower type
        List<GameObject> towerList = null;
        switch (type)
        {
            case PathEnum.Towers.Archer:
                towerList = Archers;
                break;
            case PathEnum.Towers.Sword:
                towerList = Swords;
                break;
            case PathEnum.Towers.Wizard:
                towerList = Wizards;
                break;
            default:
                Debug.LogError("Invalid tower type: " + type);
                return;
        }

        // Check if the list of tower prefabs is assigned
        if (towerList == null || towerList.Count == 0)
        {
            Debug.LogError("No tower prefabs available for the specified type: " + type);
            return;
        }

        // Check if the specified level is within the range of the tower prefab list
        if ((int)level < 0 || (int)level >= towerList.Count)
        {
            Debug.LogError("Invalid level for the specified tower type: " + level);
            return;
        }

        // Determine if it's a tower purchase or a tower sale based on the site level
        bool isSelling = ((int)level == 0);

        // Calculate the cost based on tower type and level
        int cost = GetCost(type, level, isSelling);

        // Adjust credits based on tower purchase or sale
        if (isSelling)
        {
            AddCredits(cost);
        }
        else
        {
            if (GetCredits() < cost)
            {
                Debug.LogWarning("Not enough credits to purchase this tower.");
                return;
            }
            RemoveCredits(cost);
        }

        // Instantiate the tower prefab at the selected site's position
        GameObject towerPrefab = towerList[(int)level];
        if (towerPrefab == null)
        {
            Debug.LogError("Tower prefab at level: " + level + " is not assigned.");
            return;
        }

        // Place the tower on the selected site
        GameObject newTower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);
        selectedSite.SetTower(newTower, level, type);

        // Hide the menu by passing null to the SetSite function in TowerMenu
        menu.GetComponent<TowerMenu>().SetSite(null);
    }

}
