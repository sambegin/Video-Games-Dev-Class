using UnityEngine;
using System.Collections;

public class MainMenuBehavior : MonoBehaviour {


    private Vector3 startGameTextHidePosition = new Vector3(-150, 10, -3);
    private Vector3 selectStageTextHidePosition = new Vector3(-150, -5, -3);
    private Vector3 quitGameTextHidePosition = new Vector3(-150, -20, -3);
    private Vector3 backTextHidePosition = new Vector3(150, -20, -3);
    private Vector3 stageOneHidePosition = new Vector3(110, 6, -3);
    private Vector3 stageTwoHidePosition = new Vector3(150, 6, -3);
    private Vector3 stageThreeHidePosition = new Vector3(190, 6, -3);

    private Vector3 startGameTextVisiblePosition = new Vector3(0, 10, -3);
    private Vector3 selectStageTextVisiblePosition = new Vector3(0, -5, -3);
    private Vector3 quitGameTextVisiblePosition = new Vector3(0, -20, -3);
    private Vector3 backTextVisiblePosition = new Vector3(0, -20, -3);
    private Vector3 stageOneVisiblePosition = new Vector3(-40, 6, -3);
    private Vector3 stageTwoVisiblePosition = new Vector3(0, 6, -3);
    private Vector3 stageThreeVisiblePosition = new Vector3(40, 6, -3);

    private string statusMenu = "normal";

    private float Speed = 100;
    private Transform startGameTransform;
    private Transform selectStageTransform;
    private Transform quitGameTransform;
    private Transform backTransform;
    private Transform stageOne;
    private Transform stageTwo;
    private Transform stageThree;
    

    public void Update ()
    {
        startGameTransform = GameObject.FindGameObjectWithTag("StartGame").transform;
        selectStageTransform = GameObject.FindGameObjectWithTag("SelectStage").transform;
        quitGameTransform = GameObject.FindGameObjectWithTag("QuitGame").transform;
        backTransform = GameObject.FindGameObjectWithTag("Back").transform;
        statusMenu = GameObject.FindGameObjectWithTag("MenuStatus").GetComponent<TextMesh>().text;
        stageOne = GameObject.FindGameObjectWithTag("StageOne").transform;
        stageTwo = GameObject.FindGameObjectWithTag("StageTwo").transform;
        stageThree = GameObject.FindGameObjectWithTag("StageThree").transform;

        if (statusMenu == "selectStage" && startGameTransform.position != startGameTextHidePosition)
        {
            startGameTransform.position = Vector3.MoveTowards(startGameTransform.position, startGameTextHidePosition, Time.deltaTime * Speed);
            selectStageTransform.position = Vector3.MoveTowards(selectStageTransform.position, selectStageTextHidePosition, Time.deltaTime * Speed);
            quitGameTransform.position = Vector3.MoveTowards(quitGameTransform.position, quitGameTextHidePosition, Time.deltaTime * Speed);
            backTransform.position = Vector3.MoveTowards(backTransform.position, backTextVisiblePosition, Time.deltaTime * Speed);
            stageOne.position = Vector3.MoveTowards(stageOne.position, stageOneVisiblePosition, Time.deltaTime * Speed);
            stageTwo.position = Vector3.MoveTowards(stageTwo.position, stageTwoVisiblePosition, Time.deltaTime * Speed);
            stageThree.position = Vector3.MoveTowards(stageThree.position, stageThreeVisiblePosition, Time.deltaTime * Speed);
        }
        else if(statusMenu == "normal" && startGameTransform.position != startGameTextVisiblePosition)
        {
            startGameTransform.position = Vector3.MoveTowards(startGameTransform.position, startGameTextVisiblePosition, Time.deltaTime * Speed);
            selectStageTransform.position = Vector3.MoveTowards(selectStageTransform.position, selectStageTextVisiblePosition, Time.deltaTime * Speed);
            quitGameTransform.position = Vector3.MoveTowards(quitGameTransform.position, quitGameTextVisiblePosition, Time.deltaTime * Speed);
            backTransform.position = Vector3.MoveTowards(backTransform.position, backTextHidePosition, Time.deltaTime * Speed);
            stageOne.position = Vector3.MoveTowards(stageOne.position, stageOneHidePosition, Time.deltaTime * Speed);
            stageTwo.position = Vector3.MoveTowards(stageTwo.position, stageTwoHidePosition, Time.deltaTime * Speed);
            stageThree.position = Vector3.MoveTowards(stageThree.position, stageThreeHidePosition, Time.deltaTime * Speed);
        }
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
            GameObject.FindGameObjectWithTag("MenuStatus").GetComponent<TextMesh>().text = "selectStage";
        }
        else if (textComponent.tag == "Back")
        {
            GameObject.FindGameObjectWithTag("MenuStatus").GetComponent<TextMesh>().text = "normal";
        }
        else
        {
            Application.LoadLevel(1);
        }

    }
}
