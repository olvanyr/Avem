using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of LoadAndSaveData in the scene");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        
    }

    public void LoadData()
    {
        
    }
    
    


}
