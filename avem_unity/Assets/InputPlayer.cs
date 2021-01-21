using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{


    public PlayerInput playerInput;

    public static InputPlayer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de InputPlayer dans la scéne");
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        playerInput = new PlayerInput();


    }
}
