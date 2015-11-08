using UnityEngine;
using System.Collections;
using System;

public class Lightable : MonoBehaviour
{
    private System.Collections.Generic.Dictionary<Material, Texture2D> allMaterialsAndTheirDarkeningLayer = new System.Collections.Generic.Dictionary<Material, Texture2D>();


    // Use this for initialization
    void Awake()
    {
        Material[] materialsOfTarget = this.GetComponent<Renderer>().materials;
        for (int indexCurrentMaterial = 0; indexCurrentMaterial < materialsOfTarget.Length; indexCurrentMaterial++)
        {
            Material currentMaterial = materialsOfTarget[indexCurrentMaterial];

            Texture2D darkeningLayer = initializeDarkeningLayerOn(currentMaterial);

            allMaterialsAndTheirDarkeningLayer.Add(currentMaterial, darkeningLayer);
        }

    }

    private static Texture2D initializeDarkeningLayerOn(Material currentMaterial)
    {
        Texture2D darkeningLayer = new Texture2D(currentMaterial.mainTexture.width, currentMaterial.mainTexture.height);
        Color[] pixelsOfDarkeningLayer = darkeningLayer.GetPixels();
        Color darkColor = new Color(0, 0, 0, 1);
        for (int indexCurrentPixel = 0; indexCurrentPixel < pixelsOfDarkeningLayer.Length; indexCurrentPixel++)
        {
            pixelsOfDarkeningLayer[indexCurrentPixel] = darkColor;
        }
        darkeningLayer.SetPixels(pixelsOfDarkeningLayer);
        darkeningLayer.Apply();

        currentMaterial.SetTexture("_PointsShotInfo", darkeningLayer);
        return darkeningLayer;
    }

    internal void lightUp(RaycastHit hitSpot)
    {
        Material material;
        try
        {
            material = findMaterialThatIsShot(hitSpot);
        }
        catch (MaterialNotFoundException exception)
        {
            Debug.LogException(exception);
            Material defaultMaterial = this.GetComponent<Renderer>().material;
            material = defaultMaterial;
        }

        int anyNumber = 1;
        Texture2D darkeningLayer = new Texture2D(anyNumber, anyNumber);
        allMaterialsAndTheirDarkeningLayer.TryGetValue(material, out darkeningLayer);

        Camera camera = FindObjectOfType<Camera>();
        Matrix4x4 matrix = camera.cameraToWorldMatrix;
        Matrix4x4 inverseMatrix = matrix.inverse;
        //TODO Change the size of the lightning area proportionnaly to the texel size on the screen.

        float coordX = hitSpot.textureCoord.x * material.mainTexture.width;
        float coordY = hitSpot.textureCoord.y * material.mainTexture.height;

        Color transparentColor = new Color(1, 1, 1, 0);
        lightUpACircle(darkeningLayer, coordX, coordY, transparentColor);
    }

    private static void lightUpACircle(Texture2D darkeningLayer, float coordX, float coordY, Color transparentColor)
    {
        //int radius = 5;

        //for (double i = 0.0; i < 360.0; i += 0.1)

        //{

        //    double angle = i * System.Math.PI / 180;

        //    int x = (int)(50 + radius * System.Math.Cos(angle));

        //    int y = (int)(50 + radius * System.Math.Sin(angle));

        //    darkeningLayer.SetPixel(x, y, transparentColor);
        //}


        darkeningLayer.SetPixel((int)coordX, (int)coordY, transparentColor);

        darkeningLayer.SetPixel((int)coordX + 2, (int)coordY, transparentColor);
        darkeningLayer.SetPixel((int)coordX + 1, (int)coordY, transparentColor);
        darkeningLayer.SetPixel((int)coordX - 1, (int)coordY, transparentColor);
        darkeningLayer.SetPixel((int)coordX - 2, (int)coordY, transparentColor);

        darkeningLayer.SetPixel((int)coordX, (int)coordY + 2, transparentColor);
        darkeningLayer.SetPixel((int)coordX, (int)coordY + 1, transparentColor);
        darkeningLayer.SetPixel((int)coordX, (int)coordY - 1, transparentColor);
        darkeningLayer.SetPixel((int)coordX, (int)coordY - 2, transparentColor);

        darkeningLayer.SetPixel((int)coordX + 1, (int)coordY + 1, transparentColor);
        darkeningLayer.SetPixel((int)coordX - 1, (int)coordY + 1, transparentColor);
        darkeningLayer.SetPixel((int)coordX - 1, (int)coordY - 1, transparentColor);
        darkeningLayer.SetPixel((int)coordX + 1, (int)coordY - 1, transparentColor);

        darkeningLayer.Apply();
    }

    private Material findMaterialThatIsShot(RaycastHit hitSpot)
    {
        int triangleIndex = hitSpot.triangleIndex;
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        int numberOfSubmeshes = mesh.subMeshCount;
        int nullIndex = -1;
        int materialIndex = nullIndex;

        int lookupIndex1 = mesh.triangles[triangleIndex * 3];
        int lookupIndex2 = mesh.triangles[triangleIndex * 3 + 1];
        int lookupIndex3 = mesh.triangles[triangleIndex * 3 + 2];

        for (int i = 0; i < numberOfSubmeshes; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for (int j = 0; j < triangles.Length; j += 3)
            {
                if (isRightTriangle(lookupIndex1, lookupIndex2, lookupIndex3, triangles, j))
                {
                    materialIndex = i;
                    break;
                }
            }
            if (materialIsFound(nullIndex, materialIndex))
            {
                break;
            }
        }

        try
        {
            return this.GetComponent<Renderer>().materials[materialIndex];
        }
        catch (IndexOutOfRangeException exception)
        {
            throw new MaterialNotFoundException(exception);
        }
    }

    private static bool materialIsFound(int nullIndex, int materialIndex)
    {
        return materialIndex != nullIndex;
    }

    private static bool isRightTriangle(int lookupIndex1, int lookupIndex2, int lookupIndex3, int[] triangles, int j)
    {
        return triangles[j] == lookupIndex1 && triangles[j + 1] == lookupIndex2 && triangles[j + 2] == lookupIndex3;
    }
}
