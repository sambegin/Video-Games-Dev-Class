using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public Text levelFinishedText;

    void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.pauseGame(true);
    }

    public void goToMainMenu()
    {
        Application.LoadLevel(0);
    }

    internal void isGameOver(bool playerIsDead)
    {
        levelFinishedText.text = "Game Over";
    }
}
