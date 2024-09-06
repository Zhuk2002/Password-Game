using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private GameObject _fadeOutPanel;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PostProcessController _cameraFadeOut;
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Transition()
    {
        StartCoroutine(DelayBeforeAnimation());
    }
    
    
    private IEnumerator DelayBeforeAnimation()
    {
        yield return new WaitForSeconds(1);
        _audioManager.Play("Transition");
        yield return new WaitForSeconds(1);
        _light.GetComponent<Animator>().Play("LightIncrease");
        _cameraFadeOut.enabled = true;
        _fadeOutPanel.SetActive(true); 
        StartCoroutine(DelayBeforeLoad());
    }
    
    private IEnumerator DelayBeforeLoad()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
