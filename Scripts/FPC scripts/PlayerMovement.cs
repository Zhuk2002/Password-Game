using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public CharacterController controller;
    public float speed;
    public float gravity;
    Vector3 velocity;
    [SerializeField] private bool isAbleToRun = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        speed = Sprint();
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        
        controller.Move(move * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        
        controller.Move(velocity * Time.deltaTime);
    }
    
    float Sprint()
    {
        if(!isAbleToRun)
        {
            return speed;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            return speed*3;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            return speed/3;
        }
        return speed;
    }
}
