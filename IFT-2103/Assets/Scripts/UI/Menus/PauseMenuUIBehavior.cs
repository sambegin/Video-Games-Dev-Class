using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuUIBehavior : MonoBehaviour {


    public void ChangeMouseEnterTextColor (Text text)
    {
        text.color = Color.black;
    }


    public void ChangeMouseExitTextColor (Text text)
    {
        text.color = Color.white;
    }
  
}
