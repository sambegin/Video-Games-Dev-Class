using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuUIBehavior : MonoBehaviour {


    public void ChangeMouseEnterTextColor (Text text)
    {
        text.color = Color.black;
    }


    public void ChangeMouseExitTextColor (Text text)
    {
        text.color = Color.white;
    }

    void OnMouseEnter()
    {
        GetComponent<TextMesh>().color = Color.black;
    }

    void OnMouseExit()
    {
        GetComponent<TextMesh>().color = Color.white;
    }

    void OnMouseUp ()
    {
       var textComponent = GetComponent<TextMesh>();
       if (textComponent.tag == "QuitGame")
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(1);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
