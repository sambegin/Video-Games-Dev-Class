using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject mapPrefab;
    public GameObject playerPrefab;
    public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
        loadMap();
        loadPlayer();
        loadPauseMenu();
        setupCursor();
    }

    private void loadMap()
    {
        //TODO Could use a service locator to register the map eventually
        Instantiate(mapPrefab, new Vector3(), new Quaternion());
    }

    private GameObject loadPlayer()
    {
        Vector3 initialPlayerPosition = new Vector3(0, 2f, 0);
        GameObject player = Instantiate(playerPrefab, initialPlayerPosition, new Quaternion()) as GameObject;
        return player;
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
