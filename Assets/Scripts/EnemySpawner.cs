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

        // Kies willekeurig tussen Path1 en Path2
        PathEnum.Path randomPath = (PathEnum.Path)Random.Range(0, 2);
        List<GameObject> selectedPath = (randomPath == PathEnum.Path.Path1) ? Path1 : Path2;

        if (selectedPath.Count == 0)
        {
            Debug.LogError(randomPath.ToString() + " is not assigned or empty.");
            return;
        }

        // Start bij de eerste variabele in de lijst van het gekozen pad
        Vector3 spawnPosition = selectedPath[0].transform.position;
        Quaternion spawnRotation = selectedPath[0].transform.rotation;

        GameObject newEnemy = Instantiate(Enemies[type], spawnPosition, spawnRotation);
        Enemy script = newEnemy.GetComponent<Enemy>();

        // Stel het pad in voor de vijand
        script.SetPath(randomPath);

        // Start bij het eerste punt van het pad
        script.SetTarget(selectedPath[0]);
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
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



        }

    }
}
