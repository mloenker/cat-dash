using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimateUI : MonoBehaviour
{
    private float counter = 0;
    private bool textState = true;
    public TextMeshProUGUI textObject;
    private string originalText;
    
    void Start()
    {
        originalText = textObject.text;   
        textObject.text = "> " + originalText + " <";
    }

    // Animate text
    void Update()
    {
        counter += Time.unscaledDeltaTime;

        if(counter>=1.0f){
            if(textState==true){
                textObject.text = ">  " + originalText + "  <";
            }else{
                textObject.text = "> " + originalText + " <";
            }
            textState = !textState;
            counter = 0;
        }
    }
}
