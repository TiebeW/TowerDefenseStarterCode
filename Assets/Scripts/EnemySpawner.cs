using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    private static EnemySpawner instance;

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SpawnEnemy(int type, Enums.Path path)
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (path == Enums.Path.Path1)
        {
            spawnPosition = Path1[0].transform.position;
            spawnRotation = Path1[0].transform.rotation;
        }
        else if (path == Enums.Path.Path2)
        {
            spawnPosition = Path2[0].transform.position;
            spawnRotation = Path2[0].transform.rotation;
        }
        else
        {
            // Handle error or default case
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
            Debug.LogError("Invalid path specified!");
            return;
        }

        var newEnemy = Instantiate(Enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);

        var script = newEnemy.GetComponentInParent<UFO>();



        // set hier het path en target voor je enemy in 

    }


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnTester()
    {

        SpawnEnemy(0, Enums.Path.Path1);

    }
}