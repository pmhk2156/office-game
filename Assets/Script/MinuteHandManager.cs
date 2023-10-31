using System.Collections;
using UnityEngine;

public class MinuteHandManager : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;

    void Update()
    {   
        GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, -360 / 60.0f * timeManager.elapsedTime);
    }
}