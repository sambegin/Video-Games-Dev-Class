using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheatCodeManager : MonoBehaviour {

    CanvasGroup cheatCodeCanvas;
    InputField inputField;
    LevelManager levelManager;

	void Start () {
        cheatCodeCanvas = GetComponent<CanvasGroup>();
        cheatCodeCanvas.interactable = false;
        cheatCodeCanvas.alpha = 0;

        inputField = GetComponentInChildren<InputField>();
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	public void cheatCodeEntered(string cheatCode)
    {
        if (cheatCode.Equals("Chuck Norris"))
        {
            levelManager.playerHasWon();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(cheatCodeCanvas.interactable == true)
            {
                cheatCodeCanvas.interactable = false;
                cheatCodeCanvas.alpha = 0;
            }
            else{
                cheatCodeCanvas.interactable = true;
                cheatCodeCanvas.alpha = 1;

                inputField.Select();
                inputField.ActivateInputField();
            }
            
        }
    }
}
