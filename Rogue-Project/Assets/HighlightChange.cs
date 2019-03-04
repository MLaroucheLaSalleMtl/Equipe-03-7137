using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightChange : MonoBehaviour
{
    private HighlightSword[] buttons;
    private int currentButton =0;
    private float timeToSwitch = 1f;
    
    void Awake()
    {
        buttons = GetComponentsInChildren<HighlightSword>();
        
    }

    void OnEnable() {
        buttons[0].CatchSword();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
