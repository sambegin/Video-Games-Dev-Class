using UnityEngine;
using System.Collections;

public class MainMenuUIBehavior : MonoBehaviour {



    private Vector3 startGameTextHidePosition = new Vector3(-150, 10, -3);
    private Vector3 selectLevelTextHidePosition = new Vector3(-150, -5, -3);
    private Vector3 quitGameTextHidePosition = new Vector3(-150, -20, -3);
    private Vector3 backTextHidePosition = new Vector3(150, -20, -3);

    private Vector3 startGameTextVisiblePosition = new Vector3(0, 10, -3);
    private Vector3 selectLevelTextVisiblePosition = new Vector3(0, -5, -3);
    private Vector3 quitGameTextVisiblePosition = new Vector3(0, -20, -3);
    private Vector3 backTextVisiblePosition = new Vector3(0, -20, -3);

    private bool inSelectLevel;

    public float SmoothFactor = 0.5f;
    public Transform startGameTransform;
    public Transform selectStageTransform;
    public Transform quitGameTransform;
    public Transform backTransform;

    public void Update()
    {
        if (inSelectLevel)
        {
            startGameTransform.position = Vector3.Lerp(startGameTransform.position, startGameTextHidePosition, Time.deltaTime * SmoothFactor);
            selectStageTransform.position = Vector3.Lerp(selectStageTransform.position, selectLevelTextHidePosition, Time.deltaTime * SmoothFactor);
            quitGameTransform.position = Vector3.Lerp(quitGameTransform.position, quitGameTextHidePosition, Time.deltaTime * SmoothFactor);
            backTransform.position = Vector3.Lerp(backTransform.position, backTextVisiblePosition, Time.deltaTime * SmoothFactor);
        }
        else
        {
            startGameTransform.position = Vector3.Lerp(startGameTransform.position, startGameTextVisiblePosition, Time.deltaTime * SmoothFactor);
            selectStageTransform.position = Vector3.Lerp(selectStageTransform.position, selectLevelTextVisiblePosition, Time.deltaTime * SmoothFactor);
            quitGameTransform.position = Vector3.Lerp(quitGameTransform.position, quitGameTextVisiblePosition, Time.deltaTime * SmoothFactor);
            backTransform.position = Vector3.Lerp(backTransform.position, backTextHidePosition, Time.deltaTime * SmoothFactor);
        }
    }

    public void SelectStage()
    {
        inSelectLevel = true;
    }

    public void BackFromSelectStage()
    {
        inSelectLevel = false;
    }


}
