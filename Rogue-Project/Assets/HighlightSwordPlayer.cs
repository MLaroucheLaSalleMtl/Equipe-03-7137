using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightSwordPlayer : MonoBehaviour
{
    private HighlightSword[] buttons;

    private int currentButton = 0;
    private float timeToSwitch = 1f;
    private int numberOfAwakeButtons = 0;
    void Awake()
    {
        buttons = GetComponentsInChildren<HighlightSword>();

        foreach (HighlightSword h in buttons)
        {
            if (h.gameObject.activeSelf)
            {
                numberOfAwakeButtons++;
            }
        }
    }

    void OnEnable()
    {
        buttons[0].CatchSwordPlayer();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Horizontal"))
        {
            currentButton = (currentButton+1) % numberOfAwakeButtons;
        }

        buttons[currentButton].CatchSword();
        if (Input.GetButtonDown("Jump"))
        {


        }
    }

    void DisableSword() {
        foreach (HighlightSword h in buttons) {
            h.sword.SetActive(false);
        }

    }
}
