using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public static Restart instance;

    public LevelLoader levelLoader;

    public GameObject explosionLight;

    private GameObject explosionInstance;

    public AudioClip nextSong;

    public AudioClip explosionSound;

    public GlobalVariables globalVar;

    public PauseMenu pauseMenu;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de Restart dans la scéne");
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        nextSong = globalVar.menuMusic;
        explosionSound = globalVar.explosion;
    }
    public void RestartGame()
    {
        var position = new Vector3(PlayerHealth.instance.transform.position.x, PlayerHealth.instance.transform.position.y, PlayerHealth.instance.transform.position.z);
        if (explosionInstance == null)
        {
            AudioManager.instance.PlayClip(explosionSound, "Sound", transform.position);
            explosionInstance = Instantiate(explosionLight, position, Quaternion.identity);
            StartCoroutine(ChangeRoom());
            pauseMenu.Resumed();
        }

        
    }

    public IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.NextSong(nextSong);
        levelLoader.LoadLevel("MainMenu");
    }
}
