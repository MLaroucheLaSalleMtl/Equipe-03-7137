using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text[] texts;
    private PlayerStateMachine player;
    private PlayerBaseClass playerStats;
    //public Text HPText;
    //public Text ManaText;
    // Start is called before the first frame update
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
        playerStats = player.PBS;
    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = playerStats.playerName;
        texts[1].text = "HP: " +playerStats.currentHP + "/" + playerStats.baseHP;
        texts[2].text = "MP: " +playerStats.currentMP + "/" + playerStats.baseMP;
        texts[3].text = "STR: "+ playerStats.Strength.ToString();
        texts[4].text = "CON: "+playerStats.Constitution.ToString();
        texts[5].text = "DEF: "+playerStats.Defense.ToString();
        texts[6].text = "INT: " +playerStats.Intelligence.ToString();
        texts[7].text = "LUC: "+playerStats.Luck.ToString();
    }
}
