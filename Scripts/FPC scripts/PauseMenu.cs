using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private FreeMouseLook mouseLook;
    [SerializeField] private FixedMouseLook fixedmMouseLook;
    private CursorLockMode currentlockState;
    private bool isPaused = false;

    void Awake()
    {
        fixedmMouseLook = GetComponent<FixedMouseLook>();
        mouseLook = GetComponent<FreeMouseLook>();
    }
    // Update is called once per frame
    void Update()
    {
        ShowOverlay();
    }
    
    void ShowOverlay()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseOverlay.SetActive(isPaused);
            if(mouseLook != null)
            {
                mouseLook.enabled = !mouseLook.enabled;
            }
            else
            {
                fixedmMouseLook.enabled = !fixedmMouseLook.enabled;
            }
            if(isPaused)
            {
                currentlockState = Cursor.lockState;
                Cursor.lockState = CursorLockMode.None;
            }
            else Cursor.lockState = currentlockState;
        }
    }
}
