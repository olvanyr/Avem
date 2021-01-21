using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public TextMeshPro textHolder;
    public SpriteRenderer spriteHolder;

    public void UpdateHolder(string text, Sprite sprite)
    {
        textHolder.text = text;
        spriteHolder.sprite = sprite;
    }
}
