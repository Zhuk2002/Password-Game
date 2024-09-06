using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private ChromaticAberration chromaticAberration;
    public float targetChroma;
    private LensDistortion lensDistortion;
    public float targetLens;
    private Exposure exposure;
    public float targetExposure;
    private FilmGrain filmGrain;
    public float targetFG;
    private Vignette vignette;
    public float targetVignette;
    public float duration;
    float speedCA, speedLD, speedEXP, speedFG, speedVG;
    public bool isFadeIn;
    public FreeMouseLook mouseLook;
    public PauseMenu pauseMenu;
    public AudioManager audioManager;
    public bool isChanging = true;

    void Awake()
    {
        SpeedUpdate();
        if(isFadeIn)
        {
            mouseLook.enabled = false;
        }
        pauseMenu.enabled = false;
    }

    void Start()
    {
        if(isFadeIn)
        {
            StartCoroutine(FadeInDelay());
        }
    }

    void Update()
    {
        if(isChanging)
        {
            ChromAberChange();
            LensDistortionChange();
            ExposureChange();
            FilmGrainChange();
            VignetteChange();
        }
    }
    
    void ChromAberChange()
    {
        volume.profile.TryGet(out chromaticAberration);
        {
            chromaticAberration.intensity.value = Mathf.MoveTowards(chromaticAberration.intensity.value, targetChroma, Time.deltaTime * speedCA);
        }
    }
    
    void LensDistortionChange()
    {
        volume.profile.TryGet(out lensDistortion);
        {
            lensDistortion.intensity.value = Mathf.MoveTowards(lensDistortion.intensity.value, targetLens, Time.deltaTime * speedLD);
        }
    }
    
    void ExposureChange()
    {
        volume.profile.TryGet(out exposure);
        {
            exposure.fixedExposure.value = Mathf.MoveTowards(exposure.fixedExposure.value, targetExposure, Time.deltaTime * speedEXP);
        }
    }
    
    void FilmGrainChange()
    {
        volume.profile.TryGet(out filmGrain);
        {
            filmGrain.intensity.value = Mathf.MoveTowards(filmGrain.intensity.value, targetFG, Time.deltaTime * speedFG);
        }
    }
    
    void VignetteChange()
    {
        volume.profile.TryGet(out vignette);
        {
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, targetVignette, Time.deltaTime * speedVG);
        }
    }
    
    public void SpeedUpdate()
    {
        volume.profile.TryGet(out chromaticAberration);
        {
            speedCA = Mathf.Abs(targetChroma - chromaticAberration.intensity.value) / duration;
        }
        volume.profile.TryGet(out lensDistortion);
        {
            speedLD = Mathf.Abs(targetLens - lensDistortion.intensity.value) / duration;
        }
        volume.profile.TryGet(out exposure);
        {
            speedEXP = Mathf.Abs(targetExposure - exposure.fixedExposure.value) / duration;
        }
        volume.profile.TryGet(out filmGrain);
        {
            speedFG = Mathf.Abs(targetFG - filmGrain.intensity.value) / duration;
        }
        volume.profile.TryGet(out vignette);
        {
            speedVG = Mathf.Abs(targetVignette - vignette.intensity.value) / duration;
        }
    }
    
    IEnumerator FadeInDelay()
    {
        audioManager.Play("Transition");
        yield return new WaitForSeconds(duration);
        mouseLook.enabled = true;
        pauseMenu.enabled = true;
        isChanging = false;
        volume.profile.TryGet(out exposure);
        {
            exposure.active = false;
        }
    }
}
