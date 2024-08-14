using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrometeoTouchInput : MonoBehaviour
{

    public bool changeScaleOnPressed = false;
    [HideInInspector]
    public bool buttonPressed = false;

    private RectTransform rectTransform;
    private Vector3 initialScale;
    private float scaleDownMultiplier = 0.85f;

    private void Start(){
      rectTransform = GetComponent<RectTransform>();
      initialScale = rectTransform.localScale;
    }

    public void ButtonDown(){
      buttonPressed = true;
      if(changeScaleOnPressed){
        rectTransform.localScale = initialScale * scaleDownMultiplier;
      }
    }

    public void ButtonUp(){
      buttonPressed = false;
      if(changeScaleOnPressed){
        rectTransform.localScale = initialScale;
      }
    }

}
