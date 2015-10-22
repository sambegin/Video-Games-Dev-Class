using UnityEngine;
using System.Collections;

public class MainMenuBehavior : MonoBehaviour {


    private Vector3 startGameTextHidePosition = new Vector3(-150, 10, -3);
    private Vector3 selectStageTextHidePosition = new Vector3(-150, -5, -3);
    private Vector3 quitGameTextHidePosition = new Vector3(150, -20, -3);

    private Vector3 startGameTextVisiblePosition = new Vector3(0, 10, -3);
    private Vector3 selectStageTextVisiblePosition = new Vector3(0, -5, -3);
    private Vector3 quitGameTextVisiblePosition = new Vector3(0, -20, -3);

    private bool inselectStage;

    public float SmoothFactor = 2;
    public Transform startGameTransform;
    public Transform selectStageTransform;
    public Transform quitGameTransform;

    public void Update ()
    {
        if (inselectStage)
        {
            startGameTransform.position = Vector3.Lerp(startGameTransform.position, startGameTextHidePosition, Time.deltaTime * SmoothFactor);
            selectStageTransform.position = Vector3.Lerp(selectStageTransform.position, selectStageTextHidePosition, Time.deltaTime * SmoothFactor);
            quitGameTransform.position = Vector3.Lerp(quitGameTransform.position, quitGameTextHidePosition, Time.deltaTime * SmoothFactor);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        Application.LoadLevel(1);
    }

    public void SelectStage()
    {
        inselectStage = true;
    }

}
