using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    private ConstructionSite selectedSite;

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
        //controleer of er een site is geselecteerd
        if (selectedSite == null)
        {
            Debug.LogWarning("Er is geen site geselecteerd om te bouwen");
            return;
        }

        //selecteer de juiste lijst met prefab torens op basis van het type toren
        List<GameObject> towerList = null;
        switch(type)
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
                Debug.LogError("Ongeldig torentype: " + type);
                return;
        }

        //controleer of de lijst met prafab torens is toegewezen
        if(towerList == null || towerList.Count == 0)
        {
            Debug.LogError("Er zijn geen prefab torens beschikbaar voor het opgegeven type: " + type);
            return;
        }

        //controleer of het opgegeven niveau binnen het bereik van de prefab torenlijst ligt
        if((int)level < 0 || (int)level >= towerList.Count)
        {
            Debug.LogError("Ongeldig niveau voor het opgegeven torentype: " + level);
            return;
        }
        //maak een toren uit de lijst van prefab torens op basis van het opgegeven niveau
        GameObject towerPrefab = towerList[(int)level];
        if(towerPrefab == null)
        {
            Debug.LogError("Prefab toren op niveau: " + level + "is niet toegewezen");
            return;
        }

        //plaats de torens op geselecteerde site
        GameObject newTower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);
        selectedSite.SetTower(newTower, level, type);

        //verberg het menu door null door te geven aan de SetSite functie in TowerMenu
        menu.GetComponent<TowerMenu>().SetSite(null);


    }
}