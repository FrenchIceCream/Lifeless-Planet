using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    void Awake()
    {
        if (FindObjectsByType<InputManager>(FindObjectsSortMode.None).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    float horizontalMoveAxis;
    float verticalMoveAxis;
    public float GetHorizontalMoveAxis() => horizontalMoveAxis;
    public float GetVerticalMoveAxis() => verticalMoveAxis;

    public void ReadMovementInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalMoveAxis = inputVector.x;
        verticalMoveAxis = inputVector.y;
    }

    float horizontalLookAxis;
    float verticalLookAxis;
    public float GetHorizontalLookAxis() => horizontalLookAxis;
    public float GetVerticalLookAxis() => verticalLookAxis;

    public void ReadLookInput(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalLookAxis = inputVector.x;
        verticalLookAxis = inputVector.y;
    }


    bool firePressed;
    bool fireHeld;
    public bool FirePressed() => firePressed;
    public bool FireHeld() => fireHeld;

    public void ReadFireInput(InputAction.CallbackContext context)
    {
        firePressed = !context.canceled;
        fireHeld = !context.canceled;
        StartCoroutine(ResetFireStart());
    }

    IEnumerator ResetFireStart()
    {
        yield return new WaitForEndOfFrame();
        firePressed = false;
    }

    bool jumpPressed;
    bool jumpHeld;
    public bool JumpPressed() => jumpPressed;
    public bool JumpHeld() => jumpHeld;    

    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        jumpPressed = !context.canceled;
        jumpHeld = !context.canceled;
        StartCoroutine(ResetJumpStart());
    }

    IEnumerator ResetJumpStart()
    {
        yield return new WaitForEndOfFrame();
        jumpPressed = false;
    }
}
