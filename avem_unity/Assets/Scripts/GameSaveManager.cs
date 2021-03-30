using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;

    public List<int> doorData = new List<int>();

    public int sceneID;
    public int sceneIDLast;

    public int playerHealth;

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de GameSaveMangaer dans la scéne");
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (sceneID != sceneIDLast)
        {
            sceneIDLast = sceneID;

        }
        
        sceneID = SceneManager.GetActiveScene().buildIndex;
        
    }

}
