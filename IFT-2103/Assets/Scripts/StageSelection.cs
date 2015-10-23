using UnityEngine;
using System.Collections;

public class StageSelection : MonoBehaviour {

    void OnMouseEnter()
    {
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Metallic", 1);
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 1);
    }

    void OnMouseExit()
    {
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Metallic", 0);
        this.gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.5f);
    }

    void OnMouseUp()
    {
        var stage = this.gameObject.tag;
        if (stage == "StageOne")
        {
            Application.LoadLevel(1);
        }
        else if (stage == "StageTwo")
        {
            Application.LoadLevel(1);
        }
        else if (stage == "StageThree")
        {
            Application.LoadLevel(1);
        }

    }
}
