using UnityEngine;
using System.Collections;

public class MainMenuUIBehavior : MonoBehaviour {

    public MainMenuBehavior mainMenuBehavior;

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
            mainMenuBehavior.QuitGame();
        }
        else if (textComponent.tag == "SelectLevel")
        {
            mainMenuBehavior.SelectStage();
        }
        else
        {
            mainMenuBehavior.StartNewGame();
        }
    }

}
