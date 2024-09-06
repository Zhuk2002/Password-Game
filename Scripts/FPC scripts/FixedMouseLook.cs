using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMouseLook : MouseLook
{
    struct CameraPosition
    {
        public float cameraRotation;
        public float cameraClamp;
        public float clampOffset;
    }
    CameraPosition[] cameraTransform = new CameraPosition[2];
    private int currentPosition;
    public Transform targetPos_0;
    public Transform targetPos_1;
    public static bool canSwitchPosition = true;
    

    void Awake()
    {
        cameraTransform[0].cameraRotation = -90f;
        cameraTransform[0].clampOffset = 5f;
        cameraTransform[1].cameraRotation = -180f;
        cameraTransform[1].clampOffset = 25f;
        currentPosition = 0;
        canSwitchPosition = true;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    void Update()
    {
        FixedCameraRotation();
        if(canSwitchPosition)
        {
            PositionSwitch();
        }
    }
    
    private void FixedCameraRotation()
    {
        float localSensivity;
        if(currentPosition == 0) localSensivity = mouseSensitivity/32f;
        else localSensivity = mouseSensitivity/2;
        float mouseX = Input.GetAxis("Mouse X") * localSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * localSensivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        
        xRotation = Mathf.Clamp(xRotation, -cameraTransform[currentPosition].clampOffset, cameraTransform[currentPosition].clampOffset);
        yRotation = Mathf.Clamp(yRotation, cameraTransform[currentPosition].cameraRotation-cameraTransform[currentPosition].clampOffset, cameraTransform[currentPosition].cameraRotation+cameraTransform[currentPosition].clampOffset);
        
        CameraRotation();
    }
    
    private void PositionSwitch()
    {
        if(Input.GetKeyDown(KeyCode.A) && currentPosition == 0)
        {
            Debug.Log("Switch left");
            currentPosition = 1;
            yRotation = cameraTransform[currentPosition].cameraRotation;
            Quaternion target = Quaternion.Euler(0f, yRotation, 0f);
            _playerBody.rotation = Quaternion.Slerp(_playerBody.transform.localRotation, target, Time.deltaTime * 1);
            Cursor.lockState = CursorLockMode.Locked;
            crosshair.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.D) && currentPosition == 1)
        {
            Debug.Log("Switch right");
            currentPosition = 0;
            yRotation = cameraTransform[currentPosition].cameraRotation;
            Quaternion target = Quaternion.Euler(0f, yRotation, 0f);
            _playerBody.rotation = Quaternion.Slerp(_playerBody.transform.localRotation, target, Time.deltaTime * 1);
            Cursor.lockState = CursorLockMode.None;
            crosshair.SetActive(false);
        }
        if(currentPosition != 0)
        {
            _playerBody.position = Vector3.Lerp(_playerBody.transform.position, targetPos_1.position, Time.deltaTime * speed/2);
        }
        else
        {
            _playerBody.position = Vector3.Lerp(_playerBody.transform.position, targetPos_0.position, Time.deltaTime * speed/2);
        }  
    }
}
