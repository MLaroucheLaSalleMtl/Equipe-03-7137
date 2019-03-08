using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public AudioSource myFx;      
    public AudioClip soundFx;
    
    private void Start()
    {
        myFx = GetComponent<AudioSource>();
        myFx.Play();
    }    

    public void fxSound()
    {
        myFx.PlayOneShot(soundFx);        
    }
}
