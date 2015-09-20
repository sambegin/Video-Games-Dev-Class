using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject mapPrefab;
    public GameObject playerPrefab;
    public Camera cameraPrefab;

	// Use this for initialization
	void Start () {
        startGame();
	}

    private void startGame()
    {
        loadMap();
        GameObject player = loadPlayer();
        LoadCamera(player);
    }

    private void loadMap()
    {
        //TODO Could use a service locator to register the map eventually
        Instantiate(mapPrefab, new Vector3(), new Quaternion());
    }

    // Update is called once per frame
    void Update () {
    }

    private void LoadCamera(GameObject target)
    {
        Camera camera = Instantiate(cameraPrefab, new Vector3(0, 0, 0), new Quaternion()) as Camera;
        camera.GetComponent<FollowCamera>().setTarget(target);
    }

    private GameObject loadPlayer()
    {
        Vector3 initialPlayerPosition = new Vector3(0, 2f, 0);
        GameObject player = Instantiate(playerPrefab, initialPlayerPosition, new Quaternion()) as GameObject;
        return player;
    }
}
