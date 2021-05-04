﻿/*using UnityEngine;
using UnityEditor;
using System.Collections;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010

//Modified by Kristian Helle Jespersen
//June 2011

public class ReplaceGameObjects : ScriptableWizard
{
    
    public bool copyValues = true;
    public GameObject NewType;
    public GameObject[] OldObjects;

    [MenuItem("Custom/Replace GameObjects")]


    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
    }

    [System.Obsolete]
    void OnWizardCreate()
    {
        //Transform[] Replaces;
        //Replaces = Replace.GetComponentsInChildren<Transform>();

        foreach (GameObject go in OldObjects)
        {
            GameObject newObject;
            newObject = (GameObject)EditorUtility.InstantiatePrefab(NewType);
            newObject.transform.position = go.transform.position;
            newObject.transform.rotation = go.transform.rotation;
            newObject.transform.localScale = go.transform.localScale;
            //newObject.transform.parent = go.transform.parent;
            SpriteRenderer renderer = newObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            SpriteRenderer renderergo = go.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
            if (renderer != null && renderergo != null)
            {
                renderer.sprite = renderergo.sprite;
                renderer.sortingLayerName = renderergo.sortingLayerName;
            }
            newObject.transform.parent = go.transform.parent;
            DestroyImmediate(go);

        }

    }
    
}*/