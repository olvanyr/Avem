using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;

    public GameObject firstSelectedButton, firstSettingSelectedButton, optionClosedButton;

    public AudioClip highlightSound;
    public AudioClip onClickSound;

    public AudioClip StartGameSong;

    public AudioMixer audioMixer;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void StartGameButton() //use this function to set all starting param, but not to change scene
    {
        AudioManager.instance.NextSong(StartGameSong);  
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

    public void QuitGameButton()
    {
        Application.Quit();
    }
    
    public void OnClickSound()
    {
        AudioManager.instance.PlayClipAt(onClickSound, "Sound", transform.position);
    }
    public void HighlightSound()
    {
        AudioManager.instance.PlayClipAt(highlightSound, "Sound", transform.position);
    }
}
