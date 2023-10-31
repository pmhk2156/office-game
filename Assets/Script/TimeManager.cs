using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float elapsedTime { get; set; }
    public bool canTimePass { get; set; }

    void Awake()
    {
        canTimePass = true;
        elapsedTime = 0f;
    }

    void Update()
    {
        if (canTimePass)
        {
            elapsedTime += Time.deltaTime;
        }
    }
}