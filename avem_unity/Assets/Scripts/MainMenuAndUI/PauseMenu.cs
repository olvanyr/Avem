using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject settingsWindow;

    public GameObject controlsWindow;

    private PlayerInput playerInput;

    public GameObject firstSelectedButton, firstSettingSelectedButton, firstControlSelectedButton, optionClosedButton, controlClosedButton;

    public AudioClip onClickSound;

    public LevelLoader levelLoader;

    public string sceneId = "MainMenu";

    

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Menu.Pause.performed += _ => PauseManager();
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSettingSelectedButton);
        OnClickSound();
    }
    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionClosedButton);
        OnClickSound();
    }

    public void ControlsButton()
    {
        controlsWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstControlSelectedButton);
        OnClickSound();
    }
    public void CloseControlsButton()
    {
        controlsWindow.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlClosedButton);
        OnClickSound();
    }


    private void OnEnable() //if the script is enable, the we enable the PlayerInput we need to enable it
    {
        playerInput.Enable();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    private void OnDisable() //if the script is disable, the we disable the PlayerInput
    {
        playerInput.Disable();
    }

    public void OnClickSound()
    {
        AudioManager.instance.PlayClipAt(onClickSound, "Sound", transform.position); 
    }
}
