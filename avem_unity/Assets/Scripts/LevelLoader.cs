using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;


    public Animator transition;
    public float transitionTime = 1;

    public void LoadLevel(string sceneId)
    {
        StartCoroutine(LoadAsynchronousy(sceneId));
    }

    IEnumerator LoadAsynchronousy(string sceneId)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //load the scene in the background while our current scene is still running
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId); //while loading the scene unity give us information in a Asyncoperation obect

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            //Debug.Log("progress : "+ progress);
            slider.value = progress;

            progressText.text = progress*100 + " %";

            yield return null;
        }
    }
}
