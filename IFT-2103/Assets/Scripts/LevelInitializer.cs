using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelInitializer : MonoBehaviour {

    public GameObject mapPrefab;
    public GameObject playerPrefab;
    public GameObject pauseMenu;
    public GameObject HUD;

	void Start () {
        loadMap();
        loadHud();
        loadPlayer();
        loadPauseMenu();
        setupCursor();
    }

    private void loadHud()
    {
        Instantiate(HUD);
    }

    private void loadMap()
    {
        //TODO Could use a service locator to register the map eventually
        Instantiate(mapPrefab, new Vector3(), new Quaternion());
    }

    private void loadPlayer()
    {
        Vector3 initialPlayerPosition = new Vector3(0, 2f, 0);
        Instantiate(playerPrefab, initialPlayerPosition, new Quaternion());
    }

    private void loadPauseMenu()
    {
        Instantiate(pauseMenu);
    }

    private void setupCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
