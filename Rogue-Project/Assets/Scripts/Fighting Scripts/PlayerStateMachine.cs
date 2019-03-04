using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStateMachine2 : MonoBehaviour
{
    public PlayerBaseClass playerBaseClass;
    public enum TurnState
    {
        ADDTOLIST,
        PROCESSING,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }
    public TurnState currentState;

    //Health as a progress Bar.//
    private float current_Cooldown = 0.0f;
    private float max_Cooldown = 5.0f;
    //  public Image ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        switchState();
    }
    void UpgradeProgressBar()
    {
        current_Cooldown = current_Cooldown + Time.deltaTime;
        float calc_Cooldown = current_Cooldown / max_Cooldown;
        //ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_Cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);

        if (current_Cooldown >= max_Cooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    void switchState()
    {
        switch (currentState)
        {
            case (TurnState.ADDTOLIST):
                break;

            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.WAITING):
                break;

            case (TurnState.SELECTING):
                break;

            case (TurnState.ACTION):

                break;

            case (TurnState.DEAD):
                break;
        }
    }
}
