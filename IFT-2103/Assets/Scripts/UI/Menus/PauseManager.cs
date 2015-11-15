using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PauseManager : MonoBehaviour {

    public GameObject pausePanel;
    public bool isPaused;
    LevelManager levelManager;

    void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
        isPaused = false;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPauseValue();
            pauseGame();
        }
    }

    private void pauseGame()
    {
        levelManager.pauseGame(isPaused);
        pausePanel.SetActive(isPaused);
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
