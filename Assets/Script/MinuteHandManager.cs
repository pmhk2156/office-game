using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteHandManager : MonoBehaviour
{
    public static float elapsedTime;

    void Awake(){
        elapsedTime = 0f;
    }

    void Update()
    {   
        if(BossManager.canTimePass){
            elapsedTime += Time.deltaTime;
        }
        GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, -360 / 60.0f * elapsedTime);
    }
}

//うんちうんち