using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    #region Variables
    public SoundClass[] sound;
    public static AudioManager instance;
    #endregion

    #region Awake, Start
    void Awake()
    {
        foreach(SoundClass sounds in sound)
        {
            sounds.source = gameObject.AddComponent<AudioSource>();
            sounds.source.clip = sounds.audioClip ;

            sounds.source.volume = sounds.volume;
            sounds.source.pitch = sounds.pitch;
            sounds.source.loop = sounds.loop;
        }
    }

    void Start()
    {
        Play("OnGrass");
    }
    #endregion

    #region Sound Functions
    public void Play(string name)
    {
        SoundClass sounds = Array.Find(sound, checkSound => checkSound.name == name);

        if(sounds == null)
        {
            Debug.LogWarning("Sound: " + name + " Not found!");
            return;
        }

            sounds.source.Play();
    }
    public void Stop()
    {
        foreach(SoundClass sounds in sound)
        {
            sounds.source.Stop();
        }
    }
   
    public void StopPlay(string name)
    {
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play(name);
    }

    public static void CheckSound()
    {
        if (GameManager.currentLevel == 1)
        {
            FindObjectOfType<AudioManager>().StopPlay("OnGrass");
        }
        if (GameManager.currentLevel == 2)
        {
            FindObjectOfType<AudioManager>().StopPlay("OnCavern");
        }
        if (GameManager.currentLevel == 3)
        {
            FindObjectOfType<AudioManager>().StopPlay("OnSand");
        }
    }
    #endregion
}
