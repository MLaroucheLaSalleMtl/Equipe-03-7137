using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightSword : MonoBehaviour, IPointerEnterHandler
{

    public float x = 0;
    [SerializeField] private GameObject sword;



    public void OnPointerEnter(PointerEventData eventData)
    {
        Quaternion rot = new Quaternion();
        if (x > 0)
        {

            rot.eulerAngles = new Vector3(0, 180, 0);

        }
        else
        {
            rot.eulerAngles = new Vector3(0, 0, 0);
        }
        sword.transform.rotation = rot;
        sword.transform.position= transform.position + new Vector3(x, 0, 0);

    }
}
    
