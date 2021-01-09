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

    public AudioMixer audioMixer;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void StartGameButton() //this one is obsolote, now I use a levelLoader
    {
        SceneManager.LoadScene(levelToLoad);
        OnClickSound();
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
