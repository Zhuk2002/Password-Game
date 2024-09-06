using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseLook : MonoBehaviour
{
    public static float mouseSensitivity = 400f;
    protected float speed = 8f;
    protected float xRotation = 0f;
    protected float yRotation = 0f;
    [SerializeField] protected Transform _playerBody;
    [SerializeField] protected Transform _flashLight;
    public GameObject crosshair;
    
    protected void CameraRotation()
    {
        Quaternion target = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * speed);
        
        target = Quaternion.Euler(0f, yRotation, 0f);
        _playerBody.rotation = Quaternion.Slerp(_playerBody.transform.localRotation, target, Time.deltaTime * speed);
        
        if(_flashLight)
        {
            _flashLight.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
