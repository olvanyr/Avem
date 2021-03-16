using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    public GameObject controlsWindow;

    private PlayerInput playerInput;


    public LevelLoader levelLoader;

    public string sceneId = "MainMenu";



    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Menu.Pause.performed += _ => PauseManager();
    }
    public void PauseManager()
    {
        if (gameIsPaused)
        {
            Resumed();
        }
        else
        {
            Paused();
        }
        //Debug.Log("game is paused :" + gameIsPaused);
    }

    void Paused()
    {
        //activate our pause menu
        pauseMenuUI.SetActive(true);
        //stop other fonction of the game
        Time.timeScale = 0;
        PlayerMovement.instance.enabled = false;
        //change game status toggle pause game mod
        gameIsPaused = true;
    }

    public void Resumed()
    {
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(false);
        controlsWindow.SetActive(false);
        Time.timeScale = 1;
        PlayerMovement.instance.enabled = true;
        gameIsPaused = false;
    }


    
    public void LoadMainMenu()
    {
        Resumed();
        levelLoader.LoadLevel(sceneId);
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }
    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
    }

    public void ControlsButton()
    {
        controlsWindow.SetActive(true);
    }
    public void CloseControlsButton()
    {
        controlsWindow.SetActive(false);
    }


    private void OnEnable() //if the script is enable, the we enable the PlayerInput we need to enable it
    {
        playerInput.Enable();
    }

    private void OnDisable() //if the script is disable, the we disable the PlayerInput
    {
        playerInput.Disable();
    }
}
