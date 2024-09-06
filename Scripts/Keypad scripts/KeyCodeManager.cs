using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCodeManager : MonoBehaviour
{
    [SerializeField] private string password;
    public TextMeshProUGUI keyOutput;
    private string keyCode = "";
    private bool isFull = false;
    private bool isBlocked = false;
    
    public AudioSource btnSound;
    public AudioSource wrongPassword;
    public AudioSource correctPassword;
    
    public SceneManagment sceneManagment;
    // Start is called before the first frame update
    void Start()
    {
        keyOutput.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(keyCode.Length == 4) isFull = true;
        else isFull = false;
        keyOutput.text = keyCode;
    }
    
    public void Digit(string num)
    {
        if(isFull || isBlocked)
        {
            return;
        }
        btnSound.Play();
        keyCode += num;
    }
    
    public void Clear()
    {
        if(isBlocked)
        {
            return;
        }
        btnSound.Play();
        keyCode = "";
    }
    
    public void KeyCheck()
    {
        if(isBlocked)
        {
            return;
        }
        if(!string.Equals(keyCode, password))
        {
            wrongPassword.Play();
            keyCode = "_ERR";
            keyOutput.color = Color.red;
            isBlocked = true;
            StartCoroutine(ErrorTimer());
            return;
        }
        correctPassword.Play();
        keyOutput.color = Color.green;
        isBlocked = true;
        FixedMouseLook.canSwitchPosition = false;
        sceneManagment.Transition();
    }
    
    private IEnumerator ErrorTimer()
    {
        yield return new WaitForSeconds(1f);
        keyCode = "";
        keyOutput.text = keyCode;
        keyOutput.color = Color.white;
        isBlocked = false;
    }
}
