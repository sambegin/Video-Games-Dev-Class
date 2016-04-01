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
    public Camera mainCameraPrefab;
    public GameObject mobileControlsCanvas;
    private GameObject playerPrefabInstantiated;


    void Start () {
        loadMap();
        loadHud();
        loadPlayer();
        loadPauseMenu();
        loadMainCamera();
        setupCursor();
        #if UNITY_ANDROID
            loadMobileControls();
        #endif
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
        this.playerPrefabInstantiated = (GameObject)Instantiate(playerPrefab, initialPlayerPosition, new Quaternion());
    }

    private void loadPauseMenu()
    {
        Instantiate(pauseMenu);
    }

    private void loadMainCamera()
    {
        Instantiate(mainCameraPrefab);
    }

    private void setupCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void loadMobileControls()
    {
        GameObject instance = (GameObject)Instantiate(mobileControlsCanvas);
        MobileControlsCanvas mobileControlsInstance = instance.GetComponent<MobileControlsCanvas>();
        PlayerController playerController = playerPrefabInstantiated.GetComponent<PlayerController>();
        mobileControlsInstance.initialize(playerController);
    }

}
