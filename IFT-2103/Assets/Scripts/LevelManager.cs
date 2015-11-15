using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    public GameObject gameOverCanvas;

    internal void playerHasWon()
    {
        Instantiate(gameOverCanvas);
    }

    public void pauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;
            enableCursor();
        }
        else
        {
            Time.timeScale = 1.0f;
            disableCursor();
        }     
    }

    private void disableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void enableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
