using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightChange : MonoBehaviour
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
        buttons[0].CatchSword();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            currentButton = (currentButton - (int)Input.GetAxisRaw("Vertical")) % numberOfAwakeButtons;
            if (currentButton < 0)
            {
                currentButton = 5;
            }
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            currentButton = (currentButton + 3) % numberOfAwakeButtons;
        }

        buttons[currentButton].CatchSword();
        if (Input.GetButtonDown("Jump"))
        {
            var eventSystem = EventSystem.current;
            ExecuteEvents.Execute(buttons[currentButton].gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);

        }
    }
}
