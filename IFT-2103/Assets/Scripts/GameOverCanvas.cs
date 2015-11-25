using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOverCanvas : MonoBehaviour
{

    private Button buttonToReturnToMenu;

    void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.pauseGame(true);
    }



    public void goToMainMenu()
    {
        Application.LoadLevel(0);
    }
}
