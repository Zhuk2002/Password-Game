using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HeartBeat : MonoBehaviour
{
    
    private float timer;
    public float interval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval){
            FindObjectOfType<AudioManager>().Play("Heart");
            timer = 0f;
        }
    }
}
