using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("il y a plus d'une instance de AudioManager dans la scéne");
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }


    public AudioClip[] playlist;
    public AudioSource audioSource;
    private AudioMixerGroup audioMixerGroupe;
    public AudioMixerGroup audioMixerGroupeSound;
    public AudioMixerGroup audioMixerGroupeMusic;

    private int musicIndex = 0;

    void Start()
    {
        if (playlist.Length > 0)
        {
            audioSource.clip = playlist[musicIndex];
            audioSource.Play();

        }
    }

    void Update()
    {
        if (playlist.Length>0)
        {
            if (!audioSource.isPlaying)
            {
                PlayNextSong();
            }
        }
        
    }

    void PlayNextSong()
    {

        musicIndex = (musicIndex + 1) % playlist.Length;

        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, string audioMixerString, Vector3 pos)
    {

        if (audioMixerString == "Sound")
        {
            audioMixerGroupe = audioMixerGroupeSound;
        }

        if (audioMixerString == "Music")
        {
            audioMixerGroupe = audioMixerGroupeMusic;
        }
        GameObject tempGameObject = new GameObject("TempAudio"); //create a new empty game object named TempAudio
        tempGameObject.transform.position = pos;//change the position of the object to our param pos
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>(); //add an audiosource to the object while storing the accesse to the audiosource in a temporary var called audioSource
        audioSource.clip = clip; //add the clip we whant the object to play
        audioSource.outputAudioMixerGroup = audioMixerGroupe;//the the audio groupe so the volume is still affected by the settings
        audioSource.Play();//play the clip
        Destroy(tempGameObject, clip.length);//destroy the object when the clip is over
        return audioSource;
    }
}
