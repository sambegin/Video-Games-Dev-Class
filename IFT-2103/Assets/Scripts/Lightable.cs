using UnityEngine;
using System.Collections;
using System;

public class Lightable : MonoBehaviour
{
    private System.Collections.Generic.Dictionary<Material, Texture2D> materialsStored = new System.Collections.Generic.Dictionary<Material, Texture2D>();
    //private Texture2D pointsShotInfo;


    // Use this for initialization
    void Awake()
    {
        Debug.Log(this.name + " now awake");
        Material[] materials = this.GetComponent<Renderer>().materials;
        for (int j = 0; j < materials.Length; j++)
        {
            Debug.Log("Found material");
            Material material = materials[j];
            Texture2D darkeningLayer = new Texture2D(material.mainTexture.width, material.mainTexture.height);
            material.SetTexture("_PointsShotInfo", darkeningLayer);
            //Initialise to all black
            Color[] pixels = darkeningLayer.GetPixels();
            Debug.Log(material.name+": " + pixels);
            Color darkColor = new Color(0, 0, 0, 1);
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = darkColor;
            }
            darkeningLayer.SetPixels(pixels);
            darkeningLayer.Apply();

            
            


            materialsStored.Add(material, darkeningLayer);//Should i store pointers?
        }

        //Material material = this.GetComponent<Renderer>().material;
        //pointsShotInfo = new Texture2D(material.mainTexture.width, material.mainTexture.height);


    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void lightUp(RaycastHit hitSpot)
    {
        Material material = findMaterialThatIsShot(hitSpot);

        int anyNumber = 1;
        Texture2D pointsShotInfo = new Texture2D(anyNumber, anyNumber);
        materialsStored.TryGetValue(material, out pointsShotInfo);

        float coordX = hitSpot.textureCoord.x * material.mainTexture.width;
        float coordY = hitSpot.textureCoord.y * material.mainTexture.height;

        Color transparentColor = new Color(1, 1, 1, 0);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY, transparentColor);

        pointsShotInfo.SetPixel((int)coordX + 2, (int)coordY, transparentColor);
        pointsShotInfo.SetPixel((int)coordX + 1, (int)coordY, transparentColor);
        pointsShotInfo.SetPixel((int)coordX - 1, (int)coordY, transparentColor);
        pointsShotInfo.SetPixel((int)coordX - 2, (int)coordY, transparentColor);

        pointsShotInfo.SetPixel((int)coordX, (int)coordY + 2, transparentColor);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY + 1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY - 1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY - 2, transparentColor);

        pointsShotInfo.SetPixel((int)coordX + 1, (int)coordY+1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX - 1, (int)coordY + 1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX - 1, (int)coordY - 1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX + 1, (int)coordY - 1, transparentColor);

        pointsShotInfo.Apply();
    }

    private Material findMaterialThatIsShot(RaycastHit hitSpot)
    {
        int triangleIndex = hitSpot.triangleIndex;
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        int submeshesCount = mesh.subMeshCount;
        int materialIndex = -1;

        for(int i=0; i < submeshesCount; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for(int j=0; j < triangles.Length; j++)
            {
                if(triangles[j] == triangleIndex)
                {
                    materialIndex = i;
                    break;
                }
            }
        }

        if(materialIndex != -1)
        {
            Debug.Log(this.GetComponent<Renderer>().materials[materialIndex]);
        }
       



        return this.GetComponent<Renderer>().material;
    }
}
