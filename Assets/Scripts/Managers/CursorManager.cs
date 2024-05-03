using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Handles management of the cursor and its state
/// </summary>
public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    public enum CursorState { FPS, Menu, FPSVisable};
    [SerializeField] CursorState startState = CursorState.FPS;

    void Awake()
    {
        if (FindObjectsByType<CursorManager>(FindObjectsSortMode.None).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            ChangeCursorMode(startState);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeCursorMode(CursorState cursorState)
    {
        switch (cursorState)
        {
            case CursorState.FPS:
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            }

            case CursorState.FPSVisable:
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                break;
            }

            case CursorState.Menu:
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            }
        }
    }
}
