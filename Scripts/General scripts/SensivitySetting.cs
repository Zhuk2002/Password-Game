using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensivitySetting : MonoBehaviour
{
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = MouseLook.mouseSensitivity;
    }
    
    public static void SetSensivity(float sensivity)
    {
        MouseLook.mouseSensitivity = sensivity;
    }
}
