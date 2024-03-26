using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    private int ufoCounter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void SpawnEnemy(int type)
    {
        if (type < 0 || type >= Enemies.Count)
        {
            Debug.LogError("Invalid enemy type index.");
            return;
        }

        // Choose randomly between Path1 and Path2
        PathEnum.Path randomPath = (PathEnum.Path)Random.Range(0, 2);
        List<GameObject> selectedPath = (randomPath == PathEnum.Path.Path1) ? Path1 : Path2;

        if (selectedPath.Count == 0)
        {
            Debug.LogError(randomPath.ToString() + " is not assigned or empty.");
            return;
        }

        // Start at the first waypoint in the list of the chosen path
        Vector3 spawnPosition = selectedPath[0].transform.position;
        Quaternion spawnRotation = selectedPath[0].transform.rotation;

        GameObject newEnemy = Instantiate(Enemies[type], spawnPosition, spawnRotation);
        Enemy script = newEnemy.GetComponent<Enemy>();

        // Set the path for the enemy
        script.SetPath(randomPath);

        // Start at the first point of the path
        script.SetTarget(selectedPath[0]);
    }

    public void StartWave(int number)
    {
        // reset counter 
        ufoCounter = 0;

        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
            case 2:
                InvokeRepeating("StartWave2", 1f, 1.5f);
                break;
            case 3:
                InvokeRepeating("StartWave3", 1f, 1.5f);
                break;
            case 4:
                InvokeRepeating("StartWave4", 1f, 1.5f);
                break;
            case 5:
                InvokeRepeating("StartWave5", 1f, 1.5f);
                break;
            case 6:
                InvokeRepeating("StartWave6", 1f, 1.5f);
                break;
            default:
                Debug.LogError("Invalid wave number.");
                break;
        }
    }

    public void StartWave1()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnEnemy(0);
        }
        else
        {
            // the last Enemy will be level 2 
            SpawnEnemy(1);
        }

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    public void StartWave2()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 40)
        {
            SpawnEnemy(1);
        }
        else
        {
            // the last Enemy will be level 3 
            SpawnEnemy(2);
        }

        if (ufoCounter > 50)
        {
            CancelInvoke("StartWave2");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    public void StartWave3()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 60)
        {
            SpawnEnemy(2);
        }
        else
        {
            // the last Enemy will be level 3 
            SpawnEnemy(3);
        }

        if (ufoCounter > 50)
        {
            CancelInvoke("StartWave3");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    public void StartWave4()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 70)
        {
            SpawnEnemy(3);
        }
        else
        {
            // the last Enemy will be level 4
            SpawnEnemy(4);
        }

        if (ufoCounter > 75)
        {
            CancelInvoke("StartWave4");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    public void StartWave5()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 80)
        {
            SpawnEnemy(4);
        }
        else
        {
            // the last Enemy will be level 4
            SpawnEnemy(5);
        }

        if (ufoCounter > 75)
        {
            CancelInvoke("StartWave5");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    public void StartWave6()
    {
        ufoCounter++;

        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 90)
        {
            SpawnEnemy(6);
        }
        else
        {
            // the last Enemy will be level 5
            SpawnEnemy(7);
        }

        if (ufoCounter > 80)
        {
            CancelInvoke("StartWave6");
            // Access the instance of GameManager and call the EndWave method
            GameManager.instance.EndWave();
        }
    }

    void Start()
    {
        // Initialization code can go here if needed
    }
}
