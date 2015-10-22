using UnityEngine;
using System.Collections;

public class MainMenuBehavior : MonoBehaviour {


    private Vector3 startGameTextHidePosition = new Vector3(-150, 10, -3);
    private Vector3 selectLevelTextHidePosition = new Vector3(-150, -5, -3);
    private Vector3 quitGameTextHidePosition = new Vector3(-150, -20, -3);
    private Vector3 backTextHidePosition = new Vector3(150, -20, -3);

    private Vector3 startGameTextVisiblePosition = new Vector3(0, 10, -3);
    private Vector3 selectLevelTextVisiblePosition = new Vector3(0, -5, -3);
    private Vector3 quitGameTextVisiblePosition = new Vector3(0, -20, -3);
    private Vector3 backTextVisiblePosition = new Vector3(0, -20, -3);


    private bool inSelectLevel;

    public float SmoothFactor = 2;
    public Transform startGameTransform;
    public Transform selectLevelTransform;
    public Transform quitGameTransform;
    public Transform backTransform;

    public void Update ()
    {
        if (inSelectLevel)
        {
            startGameTransform.position = Vector3.Lerp(startGameTransform.position, startGameTextHidePosition, Time.deltaTime * SmoothFactor);
            selectLevelTransform.position = Vector3.Lerp(selectLevelTransform.position, selectLevelTextHidePosition, Time.deltaTime * SmoothFactor);
            quitGameTransform.position = Vector3.Lerp(quitGameTransform.position, quitGameTextHidePosition, Time.deltaTime * SmoothFactor);
            backTransform.position = Vector3.Lerp(backTransform.position, backTextVisiblePosition, Time.deltaTime * SmoothFactor);
        }
        //else
        //{
        //    startGameTransform.position = Vector3.Lerp(startGameTransform.position, startGameTextVisiblePosition, Time.deltaTime * SmoothFactor);
        //    selectLevelTransform.position = Vector3.Lerp(selectLevelTransform.position, selectLevelTextVisiblePosition, Time.deltaTime * SmoothFactor);
        //    quitGameTransform.position = Vector3.Lerp(quitGameTransform.position, quitGameTextVisiblePosition, Time.deltaTime * SmoothFactor);
        //    backTransform.position = Vector3.Lerp(backTransform.position, backTextHidePosition, Time.deltaTime * SmoothFactor);
        //}
    }


    void OnMouseEnter()
    {
        GetComponent<TextMesh>().color = Color.black;
    }

    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = Color.white;
    }

    void OnMouseUp()
    {
        var textComponent = GetComponent<TextMesh>();
        if (textComponent.tag == "QuitGame")
        {
            Application.Quit();
        }
        else if (textComponent.tag == "SelectStage")
        {
            inSelectLevel = true;
        }
        else if (textComponent.tag == "Back")
        {
            inSelectLevel = false;
        }
        else
        {
            Application.LoadLevel(1);
        }

    }
}
