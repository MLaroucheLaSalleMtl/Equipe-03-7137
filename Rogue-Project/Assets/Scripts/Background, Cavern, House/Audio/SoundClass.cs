﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundClass 
{
    public AudioClip audioClip;

    [HideInInspector] public AudioSource source;

    public string name;

    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 3.0f)]
    public float pitch;

    public bool loop;

}
