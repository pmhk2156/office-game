using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField]
    Button reCAPTCHAButton;
    [SerializeField] Color _normalColor = new Color (255,255,255,0);
    [SerializeField] Color _hilightedColor = new Color (95,169,255,255);
    [SerializeField] Color _pressedColor = new Color (95,169,255,255);
    [SerializeField] Color _selectedColor = new Color (245,245,245,255);
    [SerializeField] Color _disabledColor = new Color (200,200,200,128);

    public static bool isreCAPTCHAButtonClicked = true;

    public void OnreCAPTCHAButtonClicked(){
        if(isreCAPTCHAButtonClicked){
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = new Color (95,169,255,255);
            colorBlock.highlightedColor = _hilightedColor;
            colorBlock.pressedColor = _pressedColor;
            colorBlock.selectedColor = _selectedColor;
            colorBlock.disabledColor = _disabledColor;
            
		    reCAPTCHAButton.colors = colorBlock;
            isreCAPTCHAButtonClicked = false;
        } else{
            ColorBlock colorBlock_ = new ColorBlock();
            colorBlock_.normalColor = new Color (255,255,255,0);
            colorBlock_.highlightedColor = _hilightedColor;
            colorBlock_.pressedColor = _pressedColor;
            colorBlock_.selectedColor = _selectedColor;
            colorBlock_.disabledColor = _disabledColor;
		    reCAPTCHAButton.colors = colorBlock_;
        }
	}
}
