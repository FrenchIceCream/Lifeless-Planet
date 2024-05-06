using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        if (AmmoTracker.instance != null)
            AmmoTracker.instance.ResetAmmo();
        
        CursorManager.instance.ChangeCursorMode(CursorManager.instance.GetStartState());
        Time.timeScale = 1;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

        CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.FPSVisible);
        InputManager.instance.GamePaused(false);

        if (AmmoTracker.instance != null)
            AmmoTracker.instance.ResetAmmo();
    }

    public void UnpauseGame(Canvas canvas)
    {
        canvas.enabled = false;
        CursorManager.instance.ChangeCursorMode(CursorManager.CursorState.FPSVisible);
        Time.timeScale = 1;
        InputManager.instance.GamePaused(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
