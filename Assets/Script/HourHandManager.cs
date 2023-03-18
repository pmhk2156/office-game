using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourHandManager : MonoBehaviour
{
    private float elapsedHourTime;
    void Update()
    {   
        elapsedHourTime =  8f + MinuteHandManager.elapsedTime / 60.0f;
        GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, -360 / 12.0f * elapsedHourTime);
    }
}
