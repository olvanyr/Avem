using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de AudioManager dans la scéne");
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }

    

    public AudioSource audioSource;
    
    private AudioMixerGroup audioMixerGroupe;
    public AudioMixerGroup audioMixerGroupeSound;
    public AudioMixerGroup audioMixerGroupeMusic;

    public Animator animator;

    public float fadeInTime;
    public float fadeOutTime;

    public AudioClip audioClip;

    
    
    private void Start()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void NextSong(AudioClip nextSong)
    {
        StartCoroutine(PlayNextSong(nextSong));
    }


    public IEnumerator PlayNextSong(AudioClip nextSong)
    {
        animator.SetBool("Fadeing",true);
        yield return new WaitForSeconds(fadeInTime);
        audioSource.Stop();
        audioSource.clip = nextSong;
        audioSource.Play();
        animator.SetBool("Fadeing", false);
        //yield return new WaitForSeconds(fadeOutTime);
        
    }
    /*  
     *  public AudioClip[] playlist;
     *  private int musicIndex = 0;
     *  void Start()
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
        }*/

    public AudioSource PlayClipAt(AudioClip clip, string audioMixerString, Vector3 pos)
    {

        
        GameObject tempGameObject = new GameObject("TempAudio"); //create a new empty game object named TempAudio
        tempGameObject.transform.position = new Vector3(pos.x, pos.y, -15f);//change the position of the object to our param pos
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>(); //add an audiosource to the object while storing the accesse to the audiosource in a temporary var called audioSource
        

        if (audioMixerString == "Sound")
        {
            audioMixerGroupe = audioMixerGroupeSound;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = 8f;
            audioSource.maxDistance = 14f;
        }

        if (audioMixerString == "Music")
        {
            audioMixerGroupe = audioMixerGroupeMusic;

            
        }

        

        audioSource.clip = clip; //add the clip we whant the object to play

        audioSource.outputAudioMixerGroup = audioMixerGroupe;//the the audio groupe so the volume is still affected by the settings
        audioSource.Play();//play the clip
        Destroy(tempGameObject, clip.length);//destroy the object when the clip is over
        return audioSource;
    }
    public AudioSource PlayClip(AudioClip clip, string audioMixerString, Vector3 pos)
    {


        GameObject tempGameObject = new GameObject("TempAudio"); //create a new empty game object named TempAudio
        tempGameObject.transform.position = new Vector3(pos.x, pos.y, -15f);//change the position of the object to our param pos
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>(); //add an audiosource to the object while storing the accesse to the audiosource in a temporary var called audioSource


        if (audioMixerString == "Sound")
        {
            audioMixerGroupe = audioMixerGroupeSound;
        }

        if (audioMixerString == "Music")
        {
            audioMixerGroupe = audioMixerGroupeMusic;


        }



        audioSource.clip = clip; //add the clip we whant the object to play

        audioSource.outputAudioMixerGroup = audioMixerGroupe;//the the audio groupe so the volume is still affected by the settings
        audioSource.Play();//play the clip
        Destroy(tempGameObject, clip.length);//destroy the object when the clip is over
        return audioSource;
    }

    public AudioSource PlayLoop(AudioClip clip, string audioMixerString, Vector3 pos)
    {


        GameObject tempGameObject = new GameObject("TempAudio"); //create a new empty game object named TempAudio
        tempGameObject.transform.position = new Vector3(pos.x, pos.y, -15f);//change the position of the object to our param pos
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>(); //add an audiosource to the object while storing the accesse to the audiosource in a temporary var called audioSource


        if (audioMixerString == "Sound")
        {
            audioMixerGroupe = audioMixerGroupeSound;
        }

        if (audioMixerString == "Music")
        {
            audioMixerGroupe = audioMixerGroupeMusic;
        }
        audioSource.loop = true;
        audioSource.clip = clip; //add the clip we whant the object to play

        audioSource.outputAudioMixerGroup = audioMixerGroupe;//the the audio groupe so the volume is still affected by the settings
        audioSource.Play();//play the clip
        //Destroy(tempGameObject, duration);//destroy the object when the duration is over
        return audioSource;
    }
}
