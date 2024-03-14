using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SiteLevel
{
    Onbebouwd,
    Level1,
    Level2,
    Level3
}


public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public SiteLevel Level { get; private set; }
    public Enums.TowerType TowerType { get; private set; }

    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);
        tower = null;
    }


    public void SetTower(GameObject newTower, SiteLevel level, Enums.TowerType type)
    {
        if (tower != null)
        {
            GameObject.Destroy(tower);
        }

        tower = newTower;
        Level = level;
        TowerType = type;
    }
}
