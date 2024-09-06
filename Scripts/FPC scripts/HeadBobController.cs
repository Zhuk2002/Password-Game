using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;
    private CharacterController controller;
    
    [Range(0, 0.5f)] public float amplitude;
    private float frequency;//PlayerMovement.speed;
    [SerializeField] private Transform _camera;
    
    private Vector3 startPos;
    
    
    void Awake()
    {
        frequency = GetComponent<PlayerMovement>().speed * 5;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = _camera.localPosition;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!enable) return;
        frequency = GetComponent<PlayerMovement>().speed * 5;
        CheckMotion();
    }
    
    private void CheckMotion()
    {
        ResetPosition();
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            _camera.localPosition += IdleMotion();
            return;
        }
        if(!controller.isGrounded) return;
        _camera.localPosition += FootStepMotion();
    }
    
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * Time.deltaTime;
        return pos;
    }
    
    private Vector3 IdleMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency/8) * amplitude/8 * Time.deltaTime;
        return pos;
    }
    
    private void ResetPosition()
    {
        if(_camera.localPosition == startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, 1 * Time.deltaTime);
    }
}
