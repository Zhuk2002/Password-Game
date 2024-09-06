using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMouseLook : MouseLook
{
    
    public Transform focusTarget;
    public bool isMouseLook = true;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        if(isMouseLook)
        {
            FreeCameraRotation();
        }
    }
    
    private void FreeCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -75f, 70f);
        
        CameraRotation();
    }
    
    public IEnumerator FocusOnTarget()
    {
        while (true)
        {
            // Get the direction from the current position to the target position
            Vector3 direction = focusTarget.position - transform.position;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.y = 0;
            targetRotation.z = 0;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, speed/8 * Time.deltaTime);
            
            targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;
            targetRotation.z = 0;
            _playerBody.rotation = Quaternion.Slerp(_playerBody.rotation, targetRotation, speed/8 * Time.deltaTime);
            yield return null;
        }
    }
}
