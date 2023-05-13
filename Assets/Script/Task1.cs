using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour
{
    [SerializeField]
    GameObject task11;
    [SerializeField]
    GameObject task12;

    public void OnButton1Clicked()
    {   
        if(task12.activeSelf)
        {
            task12.gameObject.SetActive(false);
            task11.gameObject.SetActive(true);
        }
        else if(task11.activeSelf)
        {
            task11.gameObject.SetActive(false);
            task12.gameObject.SetActive(true);
        }
    }
}
