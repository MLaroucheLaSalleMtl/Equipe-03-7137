using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBackgroundChange : MonoBehaviour
{
    [SerializeField] private Texture[] backgrounds; 
   
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        image.texture = backgrounds[GameManager.currentLevel - 1];
    }
}
