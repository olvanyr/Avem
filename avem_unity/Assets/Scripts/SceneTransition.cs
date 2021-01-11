using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPositionStorage;

    public LevelLoader levelLoader;
    public PlayerMovement playerMovement;
    public Rigidbody2D rbPlayer;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerPositionStorage.initialValue = playerPosition;
            levelLoader.LoadLevel(sceneToLoad);
            playerMovement.enabled = false;
            rbPlayer.simulated = false;
        }
    }


    void Update()
    {
        
    }
}
