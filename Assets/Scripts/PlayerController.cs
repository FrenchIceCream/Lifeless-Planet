using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float lookSpeed = 50f;
    [SerializeField] float jumpPower = 50f;

    BoxCollider boxCollider;
    Vector3 movement;
    bool isGrounded;
    private CharacterController controller;
    private InputManager inputManager;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider>();
        inputManager = InputManager.instance;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float horMovement = inputManager.GetHorizontalMoveAxis();
        //Debug.Log("Horizontal movement: " + horMovement);
        float vertMovement = inputManager.GetVerticalMoveAxis();
        bool jumpPressed = inputManager.JumpPressed();

        
        if (controller.isGrounded)
        {
            movement = new Vector3(horMovement, -10f * Time.deltaTime, vertMovement);
            movement = transform.TransformDirection(movement);
            movement *= speed;

            if (jumpPressed)
            {
                movement.y = jumpPower;
            }
        }
        else
            movement.y -= 10f * Time.deltaTime;

        controller.Move(movement * Time.deltaTime);
    }
}
