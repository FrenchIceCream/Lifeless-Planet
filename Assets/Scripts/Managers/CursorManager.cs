using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Handles management of the cursor and its state
/// </summary>
public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    public enum CursorState { Menu, FPSVisible};
    [SerializeField] CursorState startState = CursorState.FPSVisible;
    [SerializeField] Texture2D shootingCursor;
    [SerializeField] Texture2D menuCursor;

    public CursorState GetStartState() => startState;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeCursorMode(startState);
    }

    public void ChangeCursorMode(CursorState cursorState)
    {
        switch (cursorState)
        {
            case CursorState.FPSVisible:
            {
                Cursor.SetCursor(shootingCursor, new Vector2(shootingCursor.width / 2, shootingCursor.height / 2), CursorMode.ForceSoftware);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                break;
            }

            case CursorState.Menu:
            {
                Cursor.SetCursor(menuCursor, new Vector2(menuCursor.width / 2, menuCursor.height / 2), CursorMode.ForceSoftware);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            }
        }
    }
}
