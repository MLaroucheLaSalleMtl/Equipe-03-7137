using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPUi : MonoBehaviour
{
    public GameObject Canvas;
    public Text[] texts;
    public PlayerBaseClass player;
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
    }
    bool onlyOnce = true;
    public void Enable(int ExpGained)
    {
        Canvas.SetActive(true);
        if (onlyOnce)
        {
            player.Exp += ExpGained;
            player.level += player.Exp / 30;
            player.Exp = player.Exp % 30;
            onlyOnce = false;
        }
        texts[0].text = player.playerName;
        texts[1].text = "Exp Gained: " +ExpGained.ToString();
        texts[2].text ="Exp For Next Level: " + (30-player.Exp).ToString();
        texts[3].text = "Current Level: " +player.level.ToString();
    }
    public void Disable()
    {
        Canvas.SetActive(false);
        onlyOnce = true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
