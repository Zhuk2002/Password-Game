using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuButtonManager : MonoBehaviour
{
    public GameObject sceneLight;
    public GameObject mainMenu;
    public AudioSource lightSound;
    public AudioSource ambience;
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    void Start()
    {
        StartCoroutine(MainMenuDelay());
    }
    
    public void NewGame()
    {
        StartCoroutine(NewGameTrans()) ;
    }
    
    public IEnumerator NewGameTrans()
    {
        mainMenu.SetActive(false);
        ambience.Stop();
        lightSound.Play();
        sceneLight.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ExitGame()
    {
        Debug.Log("Game closed");
        Application.Quit();
    }
    
    private IEnumerator MainMenuDelay()
    {
        yield return new WaitForSeconds(2f);
        mainMenu.SetActive(true);
    }
}
