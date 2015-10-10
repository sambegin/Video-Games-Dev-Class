using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject mapPrefab;
    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        startGame();
	}

    private void startGame()
    {
        loadMap();
        GameObject player = loadPlayer();
    }

    private void loadMap()
    {
        //TODO Could use a service locator to register the map eventually
        Instantiate(mapPrefab, new Vector3(), new Quaternion());
    }

    // Update is called once per frame
    void Update () {
    }

    private GameObject loadPlayer()
    {
        Vector3 initialPlayerPosition = new Vector3(0, 2f, 0);
        GameObject player = Instantiate(playerPrefab, initialPlayerPosition, new Quaternion()) as GameObject;
        return player;
    }
}
