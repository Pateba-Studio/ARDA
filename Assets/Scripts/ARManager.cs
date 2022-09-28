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
    public GameObject popUpChooseObject;
    public List<GameObject> objects;

    public void ChooseObject(GameObject obj)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (obj == objects[i]) objects[i].SetActive(true);
            else objects[i].SetActive(false);
        }
    }

    public void SetupPopUpChooseObject()
    {
        if (popUpChooseObject.activeInHierarchy) popUpChooseObject.SetActive(false);
        else popUpChooseObject.SetActive(true);
    }
}
