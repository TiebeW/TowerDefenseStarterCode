using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    // Singleton instance
    private static HighScoreManager instance;
    public static HighScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HighScoreManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<HighScoreManager>();
                    singletonObject.name = typeof(HighScoreManager).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // Public properties
    public string PlayerName { get; set; }
    public bool GameIsWon { get; set; }

    private void Awake()
    {
        // Ensure only one instance of the HighScoreManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
