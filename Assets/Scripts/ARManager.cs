using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;
using System.Reflection;

public class ARManager : MonoBehaviour
{
    public bool isGrounded;
    public string planeUsed;

    [Space(25)]
    public GameObject planeTarget;
    public GameObject objectTarget;
    public List<GameObject> objects;

    [Space(25)]
    public Button cameraButton;
    public Button chooseButton;
    public GameObject popUpChooseObject;

    [Space(25)]
    public Sprite cameraSpawn;
    public Sprite cameraDelete;

    GameObject[] gameObjects;

    void Update()
    {
        SetUserInterface();
    }

    public void SpawnObject()
    {
        if (!objectTarget.activeInHierarchy)
        {   
            objectTarget.SetActive(true);
            cameraButton.GetComponent<Image>().sprite = cameraDelete;
        }
        else
        {
            objectTarget.SetActive(false);
            cameraButton.GetComponent<Image>().sprite = cameraSpawn;
            objectTarget = null;
        }
    }

    public void ChooseObject(int index)
    {
        for (int i = 0; i < objects.Count; i++)
            if (index == i) 
                objectTarget = objects[i];
    }

    public void SetUserInterface ()
    {
        for (int i = 0; i < planeTarget.transform.childCount; i++)
        {
            if (planeTarget.transform.GetChild(i).gameObject.activeInHierarchy) 
            {
                cameraButton.GetComponent<Image>().sprite = cameraDelete;
                break;
            }
            else
            {
                cameraButton.GetComponent<Image>().sprite = cameraSpawn;
            }
        }
    }

    public void CheckGroundPlane()
    {
        gameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in gameObjects)
        {
            if (obj.name.Contains(planeUsed) && obj.transform.GetChild(0).GetComponent<MeshRenderer>().enabled)
            {
                isGrounded = true;
                break;
            }
            else
                isGrounded = false;
        }
    }
}
