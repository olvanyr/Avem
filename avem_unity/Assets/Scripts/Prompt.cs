using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public float enable;

    public static Prompt instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de Prompt dans la scéne");
            return;
        }

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PromptEnable"))
        {
            enable = PlayerPrefs.GetFloat("PromptEnable");
            SetEnable(enable);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = 0;
            SetEnable(enable);
        }
    }

    public void SetEnable(float enable)
    {
        if (enable == 1)
        {
            gameObject.SetActive(true);
            PlayerPrefs.SetFloat("PromptEnable", 1);
        }
        if (enable == 0)
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetFloat("PromptEnable", 0);
        }
    }
}
