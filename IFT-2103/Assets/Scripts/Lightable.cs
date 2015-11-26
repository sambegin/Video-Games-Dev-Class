using UnityEngine;
using System.Threading;
using System;
using System.Collections.Generic;

public class Lightable : MonoBehaviour
{
    private const string DARKENING_LAYER_TEXTURE_NAME = "_PointsShotInfo";
    private Dictionary<int, Color[]> allMaterialsAndTheirDarkeningLayer = new Dictionary<int, Color[]>();
    private Map map;
    private static Color TRANSPARENT_COLOR = new Color(1, 1, 1, 0);
    private static int textureSize = 20;

    void Start()
    {
        map = GetComponentInParent<Map>();

        Material[] materialsOfThisObject = this.GetComponent<Renderer>().materials;
        for (int indexCurrentMaterial = 0; indexCurrentMaterial < materialsOfThisObject.Length; indexCurrentMaterial++)
        {
            Material currentMaterial = materialsOfThisObject[indexCurrentMaterial];

            Texture2D darkeningLayer = initializeDarkeningLayerOn(currentMaterial, gameObject);

            allMaterialsAndTheirDarkeningLayer.Add(currentMaterial.GetInstanceID(), darkeningLayer.GetPixels());

            map.addLightableSurface(darkeningLayer);
        }
    }

    private static Texture2D initializeDarkeningLayerOn(Material currentMaterial , GameObject gameObject)
    {
        int[] scale =  setScale(gameObject);

        int width = scale[0];
        int height = scale[1];

        Texture2D darkeningLayer = new Texture2D(textureSize, textureSize);
        Color[] pixelsOfDarkeningLayer = darkeningLayer.GetPixels();
        Color darkColor = new Color(0, 0, 0, 1);

        for (int indexCurrentPixel = 0; indexCurrentPixel < pixelsOfDarkeningLayer.Length; indexCurrentPixel++)
        {
            pixelsOfDarkeningLayer[indexCurrentPixel] = darkColor;
        }
        darkeningLayer.SetPixels(pixelsOfDarkeningLayer);
        darkeningLayer.Apply();

        currentMaterial.SetTexture(DARKENING_LAYER_TEXTURE_NAME, darkeningLayer);
        return darkeningLayer;
    }

    private static int[] setScale(GameObject gameObject)
    {
        int[] scale = new int[2];

        scale[0] = (int)gameObject.transform.localScale.x;
        scale[1] = (int)gameObject.transform.localScale.z;

        if (gameObject.transform.localScale.x < 1)
        {
            scale[0] = (int)gameObject.transform.localScale.z;
            scale[1] = (int)gameObject.transform.localScale.y;

        }
        else
        {
            if (gameObject.transform.localScale.x < 5)
            {
                scale[0] = (int)gameObject.transform.localScale.x * 10;
                scale[1] = (int)gameObject.transform.localScale.z * 10;
            }

        }
        return scale;
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

        int[] scale = setScale(gameObject);

        float coordX = hitSpot.textureCoord.x * textureSize;//scale[0];
        float coordY = hitSpot.textureCoord.y * textureSize;//scale[1];

        int numberOfTexelsChanged = 0;
        Texture2D darkeningLayer = (Texture2D)material.GetTexture(DARKENING_LAYER_TEXTURE_NAME);
        numberOfTexelsChanged = lightUpACircle(coordX, coordY, darkeningLayer);
        map.hasLightedSurface(numberOfTexelsChanged);
    }

    private static int lightUpACircle( float centerX, float centerY, Texture2D darkeningLayer)
    {
        int numberOfTexelsChanged = 0;


        float radius = 5.0f;
        float positionX = -5.0f;
        float startPositionY = -5.0f;
        float positionY;

        while (positionX <= radius)
        {
            positionY = startPositionY;
            while (positionY <= radius)
            {

                if (isInTheCircle(centerX, centerY, centerX + positionX, centerY + positionY, radius))
                {
                    if (darkeningLayer.GetPixel((int)(centerX + positionX), (int)(centerY + positionY)) != TRANSPARENT_COLOR)
                    {
                        darkeningLayer.SetPixel((int)(centerX + positionX), (int)(centerY + positionY), TRANSPARENT_COLOR);
                        numberOfTexelsChanged += 1;
                    }
                }
                positionY += 1;

            }
            positionX += 1;

        }
        darkeningLayer.Apply();
        return numberOfTexelsChanged;
    }


    private static bool isInTheCircle(float center_x, float center_y, float coordX, float coordY, float radius)
    {
        return ((((coordX - center_x) * (coordX - center_x)) + ((coordY - center_y) * (coordY - center_y))) <= radius);
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
            throw new MaterialNotFoundException("The exact material that was shot is not found", exception);
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
