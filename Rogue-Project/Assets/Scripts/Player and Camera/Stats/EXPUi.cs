using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPUi : MonoBehaviour
{
    public GameObject Canvas;
    public Text[] texts;
   
    private BattleStateMachine bsm;
    // Start is called before the first frame update
    void Start()
    {
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        Canvas.SetActive(false);
    }

    public void Enable(int ExpGained)
    {
        Canvas.SetActive(true);
        PlayerBaseClass player = bsm.psm.PBS;
        
        texts[0].text = player.playerName;
        texts[1].text = "Exp Gained: " +ExpGained.ToString();
        texts[2].text ="Exp For Next Level: " + (player.ExpNeeded-player.Exp).ToString();
        texts[3].text = "Current Level: " +player.level.ToString();
    }
    public void Disable()
    {
        Canvas.SetActive(false);
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
