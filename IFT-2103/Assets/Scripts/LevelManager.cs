﻿using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    public GameObject gameOverCanvas;
    private MouseOrbitWithZoomCamera cameraMovment;

    void Start ()
    {
        cameraMovment = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseOrbitWithZoomCamera>();
    }

    internal void playerHasWon()
    {
        Instantiate(gameOverCanvas);
    }

    public void playerIsDead()
    {
        GameObject gameOverCanvasInitialized = Instantiate(gameOverCanvas);
        GameOverCanvas canvas = gameOverCanvasInitialized.GetComponent<GameOverCanvas>();
        canvas.isGameOver(true);
    }

    public void pauseGame(bool gameState)
    {
        if (gameState)
        {
            Time.timeScale = 0.0f;
            enableCursor();
        }
        else
        {
            Time.timeScale = 1.0f;
            disableCursor();
        }     
        cameraMovment.enabled = !gameState;
       
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
