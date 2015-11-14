using UnityEngine;
using System.Collections;
using System;

public class LevelManager : MonoBehaviour {

    public GameObject gameOverCanvas;

    internal void playerHasWon()
    {
        Instantiate(gameOverCanvas);
    }
}
