using UnityEngine;
using System.Collections;

public class MobileControlsCanvas : MonoBehaviour {

    private PlayerController playerController;

    public void initialize(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void jump()
    {
        playerController.jump();
    }


}
