using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    void Awake()
    {
        instance = this;
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
    public bool JumpPressed() => jumpPressed;   

    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        jumpPressed = !context.canceled;
        StartCoroutine(ResetJumpStart());
    }

    IEnumerator ResetJumpStart()
    {
        yield return new WaitForEndOfFrame();
        jumpPressed = false;
    }

    bool boostHeld;
    public bool BoostHeld() => boostHeld;    

    public void ReadBoostInput(InputAction.CallbackContext context)
    {
        boostHeld = !context.canceled;
    }
    
    bool pausePressed;
    public bool isGamePaused = false;
    public void GamePaused(bool pause) => isGamePaused = pause;

    [SerializeField] Canvas menuCanvas;

    public void ReadPauseInput(InputAction.CallbackContext context)
    {
        pausePressed = context.action.WasPerformedThisFrame();

        if (GameObject.FindWithTag("Player").GetComponent<Health>().GetHealth() <= 0)
            return;

        if (pausePressed)
            isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.Menu);
            Time.timeScale = 0;
            menuCanvas.enabled = true;
        }
        else
        {
            CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.FPSVisible);
            Time.timeScale = 1;
            menuCanvas.enabled = false;
        }
    }

    float cycleWeaponInput;
    
    public float CycleWeaponInput() => cycleWeaponInput;

    public void ReadCycleWeaponInput(InputAction.CallbackContext context)
    {
        Vector2 mouseScrollInput = context.ReadValue<Vector2>();
        Debug.Log(mouseScrollInput);
        if (mouseScrollInput.y == 0)
        {
            cycleWeaponInput = 0;
        }
        else
        {
            cycleWeaponInput = Mathf.Sign(mouseScrollInput.y);
        }
    }

    bool nextWeaponPressed;

    public bool NextWeaponPressed() => nextWeaponPressed;

    public void ReadNextWeaponInput(InputAction.CallbackContext context)
    {
        nextWeaponPressed = !context.canceled;
        StartCoroutine("ResetNextWeaponPressedStart");
    }

    IEnumerator ResetNextWeaponPressedStart()
    {
        yield return new WaitForEndOfFrame();
        nextWeaponPressed = false;
    }

    bool previousWeaponPressed;

    public bool PreviousWeaponPressed() => previousWeaponPressed;

    public void ReadPreviousWeaponInput(InputAction.CallbackContext context)
    {
        previousWeaponPressed = !context.canceled;
        StartCoroutine("ResetPreviousWeaponPressedStart");
    }

    IEnumerator ResetPreviousWeaponPressedStart()
    {
        yield return new WaitForEndOfFrame();
        previousWeaponPressed = false;
    }
}
