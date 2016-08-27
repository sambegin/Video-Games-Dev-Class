using UnityEngine;
using System.Collections;

public class MobileControlsCanvas : MonoBehaviour {

    private PlayerController playerController;

    public void initialize(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void moveForward()
    {
        
    }

    public void moveRight()
    {
        
    }

    public void moveBackwards()
    {
        
    }

    public void moveLeft()
    {
        
    }

    public void jump()
    {
        playerController.jumpFromUI();
    }


}
