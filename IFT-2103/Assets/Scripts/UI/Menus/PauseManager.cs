using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;
    public bool isPaused;
    

	// Use this for initialization
	void Start ()
    {
        isPaused = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        PauseGame(isPaused);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPauseValue();
        }
    }

    void PauseGame(bool state)
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
        pausePanel.SetActive(state);
    }

    private void disableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void enableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SwitchPauseValue ()
    {
        isPaused = !isPaused;
    }

    public void ReturnToMainMenu ()
    {
        Application.LoadLevel(0);
    }
}
