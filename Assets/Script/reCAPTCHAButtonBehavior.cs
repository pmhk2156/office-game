using System.Collections;
using UnityEngine;

public class reCAPTCHAButtonBehavior : MonoBehaviour
{
    private bool isClicked = false;

    public void OnreCAPTCHA()
    {
        GameObject clickedButton = transform.GetChild(0).gameObject;

        if (!isClicked)
        {
            clickedButton.gameObject.SetActive(true);
            isClicked = true;
        }
        else
        {
            clickedButton.gameObject.SetActive(false);
            isClicked = false;
        }
    }
}
