using UnityEngine;
using System.Collections;

public class PauseMenuActionsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseUp()
    {
        var textComponent = GetComponent<TextMesh>();
        if (textComponent.tag == "MainMenuReturn")
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(1);
        }

    }
}
