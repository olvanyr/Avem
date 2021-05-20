using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public AudioClip nextSong;

    public GlobalVariables globalVar;

    private void Awake()
    {
        nextSong = globalVar.menuMusic;
    }
    public void ChangeRoom() 
    {
        AudioManager.instance.NextSong(nextSong);
        levelLoader.LoadLevel("MainMenu");
    }
}
