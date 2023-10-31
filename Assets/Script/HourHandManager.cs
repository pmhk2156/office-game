using System.Collections;
using UnityEngine;

public class HourHandManager : MonoBehaviour
{
    [SerializeField]
    TimeManager timeManager;

    private float elapsedHourTime;

    void Update()
    {   
        elapsedHourTime =  8f + timeManager.elapsedTime / 60.0f;
        GetComponent<Transform>().localEulerAngles = new Vector3(0, 0, -360 / 12.0f * elapsedHourTime);
    }
}
