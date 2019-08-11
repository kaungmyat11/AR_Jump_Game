using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    private CharacterController controller;

    float v, h;

    float verticalVelocity;
    float gravity = -45f;
    float jumpForce = 30f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity = gravity * Time.deltaTime;
        }

        Vector3 movement = Vector3.zero;

        movement.x = Input.GetAxis("Horizontal") * 5;
        movement.y = verticalVelocity;
        movement.z = Input.GetAxis("Vertical") * 5;

        controller.Move(movement * Time.deltaTime);
    }
}
