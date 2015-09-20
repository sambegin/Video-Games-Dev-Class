using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public GameObject mapPrefab;

	// Use this for initialization
	void Start () {
        loadMap();
	}

    private void loadMap()
    {
        //TODO Could use a service locator to register the map eventually
        Instantiate(mapPrefab, new Vector3(), new Quaternion());
    }

    // Update is called once per frame
    void Update () {
	
	}
}
