using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerController : MonoBehaviour
{

    public VariableJoystick joystickInput;
    private Rigidbody rb;

    private Vector3 playerMovement;
    private Vector3 desiredForward;
    private Vector3 Rotation;

    private Quaternion playerRotation = Quaternion.identity;

    [SerializeField] private float jumpForce;

    [SerializeField] private float forwardSpeed = 2f;
    [SerializeField] private float turnSpeed = 5f;
    //[SerializeField] private float gravityForce;

    private float v, h;

    private bool isGrounded;

    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        v = joystickInput.Vertical;
        h = joystickInput.Horizontal;
    }

    private void FixedUpdate()
    {
        PlayerMove();

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            _animator.SetTrigger("Jump");
        }

    }

    void PlayerMove()
    {
        bool hasHorizontalInput = !Mathf.Approximately(h, 0f);
        bool hasVerticalInput = !Mathf.Approximately(v, 0);
        bool isRunning = hasHorizontalInput || hasVerticalInput;
        
        playerMovement = new Vector3(h , 0 , v);
        playerMovement.Normalize();
        playerMovement *= forwardSpeed;

        rb.velocity = playerMovement;
        desiredForward = Vector3.RotateTowards(transform.forward, playerMovement, turnSpeed * Time.fixedDeltaTime , 0f);
        playerRotation = Quaternion.LookRotation(desiredForward);
        
        _animator.SetBool("isRunning", isRunning);
        _animator.SetBool("isGrounded", isGrounded);

        rb.velocity = playerMovement;
        rb.MovePosition(rb.position + playerMovement * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            gameObject.transform.SetParent(other.transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            gameObject.transform.SetParent(null);
        }
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + playerMovement * _animator.deltaPosition.magnitude);
        rb.MoveRotation(playerRotation);
    }
}
