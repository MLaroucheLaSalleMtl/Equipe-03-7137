using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SetVolume : MonoBehaviour
{

    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParam;

    public void SetVol(float vol)
    {
        Slider slide = GetComponent<Slider>();
        audioM.SetFloat(nameParam, vol);
        PlayerPrefs.SetFloat(nameParam, vol);
        PlayerPrefs.Save();
        slide.value = vol;
    }

    // Use this for initialization
    void Start()
    {
        Slider slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParam);
        slide.value = v;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
