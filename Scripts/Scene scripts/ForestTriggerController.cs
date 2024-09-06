using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ForestTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private GameObject forestSounds;
    [SerializeField] private GameObject forestAmbience;
    [SerializeField] private GameObject title;
    [SerializeField] private HDAdditionalLightData sunLight;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private FreeMouseLook mouseLook;
    [SerializeField] private Volume volume;
    [SerializeField] private PostProcessController postProcessController;
    private ColorAdjustments colorAdjustments;
    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("End/1"))
        {
            barriers[0].SetActive(true);
            StartCoroutine(GlitchEffect());
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("End/2"))
        {
            barriers[1].SetActive(true);
            barriers[2].SetActive(false);
            GetComponent<CharacterSoundController>().isHeartBeating = true;
            forestSounds.SetActive(false);
            forestAmbience.SetActive(true);
            StartCoroutine(SunChange());
            StartCoroutine(GlitchEffect());
            GetComponentInParent<PlayerMovement>().speed = 1.6f;
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("End/3"))
        {
            audioManager.Play("Glitch");
            
            StartCoroutine(CliffDebuf(GetComponentInParent<PlayerMovement>().speed/4));
            GetComponentInParent<HeadBobController>().amplitude /=2;
            
            volume.profile.TryGet(out filmGrain);
            {
                filmGrain.active = true;
            }
            postProcessController.isChanging = true;
            postProcessController.targetChroma = 1;
            postProcessController.targetLens = 0.4f;
            postProcessController.targetFG = 0.7f;
            postProcessController.targetVignette = 0.3f;
            postProcessController.SpeedUpdate();
            
            mouseLook.isMouseLook = false;
            StartCoroutine(mouseLook.FocusOnTarget());
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("End/4"))
        {
            GetComponent<CharacterSoundController>().triggerCheck = true;
        }
        else if(other.gameObject.CompareTag("End/5"))
        {
            title.SetActive(true);
            StartCoroutine(ExitListener());
            GetComponent<PlayerMovement>().enabled = false;
        }
    }
    
    IEnumerator GlitchEffect()
    {
        float currentLD;
        volume.profile.TryGet(out lensDistortion);
        {
            currentLD = lensDistortion.intensity.value;
            lensDistortion.intensity.value = -0.8f;
        }
        volume.profile.TryGet(out colorAdjustments);
        {
            colorAdjustments.active = true;
        }
        volume.profile.TryGet(out filmGrain);
        {
            filmGrain.active = true;
            filmGrain.intensity.value = 1;
        }
        audioManager.Play("Noise");
        yield return new WaitForSeconds(0.5f);
        volume.profile.TryGet(out lensDistortion);
        {
            lensDistortion.intensity.value = currentLD;
        }
        volume.profile.TryGet(out colorAdjustments);
        {
            colorAdjustments.active = false;
        }
        volume.profile.TryGet(out filmGrain);
        {
            filmGrain.active = false;
            filmGrain.intensity.value = 0;
        }
    }
    
    IEnumerator SunChange()
    {
        yield return new WaitForSeconds(0.5f);
        sunLight.intensity = 2;
        sunLight.color = Color.red;
    }
    
    IEnumerator CliffDebuf(float newSpeed)
    {
        float timer = 0;
        float currentSpeed;
        while(timer <= 15)
        {
            currentSpeed = GetComponentInParent<PlayerMovement>().speed;
            GetComponentInParent<PlayerMovement>().speed = Mathf.MoveTowards(currentSpeed, newSpeed, Time.deltaTime/8);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    
    IEnumerator ExitListener()
    {
        yield return new WaitForSeconds(5);
        while(true)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
            yield return null;
        }
    }
}
