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

    public PlayerInput playerInput;

    public bool inRange;


    public GameObject popUp;
    public PopUp popUpScript;
    public string popUpText;
    public Sprite popUpSprite;


    public void Start()
    {
        playerInput = playerMovement.playerInput;

        playerInput.NormalMovement.Action1.performed += context => GoToNextSene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            popUp.SetActive(true);
            popUpScript.UpdateHolder(popUpText, popUpSprite);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            popUp.SetActive(false);
            //popUp.UpdateHolder(popUpText, popUpSprite);
        }
    }

    public void GoToNextSene()
    {

        if (inRange)
        {
            playerPositionStorage.initialValue = playerPosition;
            levelLoader.LoadLevel(sceneToLoad);
            playerMovement.enabled = false;
            rbPlayer.simulated = false;
        }
        
    }
}
