using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Slider slider;
    
    void Awake()
    {
        float value;
        slider = GetComponent<Slider>();
        audioMixer.GetFloat("Volume", out value);
        slider.value = value;
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
