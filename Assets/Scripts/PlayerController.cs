using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float boostSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float jumpPower = 50f;
    [SerializeField] float gravity = 10f;
    [SerializeField] float jumpTimeLeniency = 0.1f;
    float timeToStopLeniency;
    bool hasJumpedOnce = false;
    bool hasKey = false;

    public void SetKey(bool haskey)
    {
        hasKey = haskey;
    }

    public bool HasKey()
    {
        return hasKey;
    }

    Vector3 movement;
    private CharacterController controller;
    private InputManager inputManager;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        inputManager = InputManager.instance;
    }

    void Update()
    {
        Move();
        RotateHorizontally();
    }

    void Move()
    {
        float horMovement = inputManager.GetHorizontalMoveAxis();
        //Debug.Log("Horizontal movement: " + horMovement);
        float vertMovement = inputManager.GetVerticalMoveAxis();
        bool jumpPressed = inputManager.JumpPressed();
        bool boostHeld = inputManager.BoostHeld();
        
        if (controller.isGrounded)
        {
            timeToStopLeniency = Time.time + jumpTimeLeniency;
            hasJumpedOnce = false;
            movement = new Vector3(horMovement, 0, vertMovement);
            movement = transform.TransformDirection(movement);
            
            if (boostHeld)
                movement *= boostSpeed;
            else
                movement *= speed;
            
            if (jumpPressed)
            {
                hasJumpedOnce = true;
                movement.y = jumpPower;
            }
        }
        else
        {
            //временной промежуток, во время которого еще можно прыгнуть, не находясь на земле (при падении)
            if (!hasJumpedOnce && jumpPressed && Time.time < timeToStopLeniency)
            {
                movement.y = jumpPower;
                hasJumpedOnce = true;
            }

            movement = new Vector3(horMovement * (boostHeld ? boostSpeed : speed), movement.y, vertMovement * speed);
            movement = transform.TransformDirection(movement);
        }
        movement.y -= gravity * Time.deltaTime;

        controller.Move(movement * Time.deltaTime);
    }

    void RotateHorizontally()
    {
        float horizontalLookAxis = inputManager.GetHorizontalLookAxis();

        transform.Rotate(0, horizontalLookAxis * rotationSpeed * Time.deltaTime, 0);
    }


}
