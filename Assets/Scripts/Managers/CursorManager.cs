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
            DontDestroyOnLoad(gameObject);
        }
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
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                break;
            }

            case CursorState.Menu:
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
            }
        }
    }
}
