using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    //Variable List.//
    int index = 0;
    public int totalCommands;

    public float yOffset = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SelectionDirection();
    }

    private void SelectionDirection()
    {
        if (Input.GetAxisRaw("Vertical") <0)
        {
            if (index < totalCommands - 1)
            {
                index++;
                Vector3 position = transform.position;
                position.y -= yOffset;
                transform.position = position;
                position.z = 0.0f;
            }
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (index > 0)
            {
                index--;
                Vector3 position = transform.position;
                position.y += yOffset;
                transform.position = position;
                position.z = 0.0f;
            }
        }
    }
}
