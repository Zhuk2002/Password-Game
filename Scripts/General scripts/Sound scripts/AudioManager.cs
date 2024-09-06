using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup output;
    public Sound[] sounds;
    public FootSteps[] footsteps;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.outputAudioMixerGroup = output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    void Start()
    {
        foreach (Sound s in sounds)
        {
            if(s.playFromStart == true)
            {
                Play(s.name);
            }
        }
    }
    
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.Play();
    }
    
    public void PlayRandomFootstep (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Footstep");
        if(s == null) return;
        if(s.source.isPlaying) return;
        FootSteps footStep = Array.Find(footsteps, sound => sound.name == name);
        s.source.clip = footStep.audioClip[UnityEngine.Random.Range(0,footStep.audioClip.Length)];
        s.source.Play();
    }
}

[System.Serializable]
public class FootSteps {
    public string name;
    public AudioClip[] audioClip;
}
