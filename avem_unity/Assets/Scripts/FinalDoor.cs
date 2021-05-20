using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public LevelLoader levelLoader;
    public PauseMenu pauseMenu;
    public AudioClip nextSong;
    public Animator animator;

    public GlobalVariables globalVar;

    public bool isInRange;
    // Start is called before the first frame update
    void Start()
    {
        nextSong = globalVar.endMusic;

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            pauseMenu.Resumed();
            PlayerMovement.instance.state = "exit";
            Restart.instance.enabled = false;
            animator.SetTrigger("Open");
        }
    }
    public void ChangeRoom()
    {
        AudioManager.instance.NextSong(nextSong);
        levelLoader.LoadLevel("End");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

}
