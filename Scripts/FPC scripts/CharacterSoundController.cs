using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSoundController : MonoBehaviour
{
    private AudioManager _audioManager;
    
    //HeartBeat
    public float hbInterval = 1;
    private float _hbTimer;
    private string _hbName = "Heart";
    public bool triggerCheck = false;
    public bool isHeartBeating = true;
    
    //Footsteps
    private float frequency;
    
    void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        frequency = GetComponent<PlayerMovement>().speed * 5f;
    }

    void Update()
    {
        HeartBeating();
        FootStep();
    }
    
    void HeartBeating()
    {
        _hbTimer += Time.deltaTime;
        TriggerCheck();
        if(_hbTimer >= hbInterval && isHeartBeating){
            _audioManager.Play(_hbName);
            _hbTimer = 0f;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Screamer"){
            triggerCheck = true;
        }
    }
    
    void TriggerCheck()
    {
        if(triggerCheck){
            _hbName = "Hearthigh";
            hbInterval = 0.4f;
        }
        else{
            _hbName = "Heart";
            hbInterval = 1f;
        }
    }
    
    void FootStep()
    {
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            return;
        }
        if(Mathf.Sin(Time.time * frequency) <= -0.95f){
            Sound s = Array.Find(_audioManager.sounds, sound => sound.name == name);
            _audioManager.PlayRandomFootstep("Forest");
        }
        frequency = GetComponent<PlayerMovement>().speed * 5f;
    }
}
