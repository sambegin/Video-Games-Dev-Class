using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Lightable : MonoBehaviour
{
    private Dictionary<int, Color[]> allMaterialsAndTheirDarkeningLayer = new Dictionary<int, Color[]>();
    private Map map;

    void Start()
    {
        map = GetComponentInParent<Map>();

        Material[] materialsOfTarget = this.GetComponent<Renderer>().materials;
        for (int indexCurrentMaterial = 0; indexCurrentMaterial < materialsOfTarget.Length; indexCurrentMaterial++)
        {
            Material currentMaterial = materialsOfTarget[indexCurrentMaterial];

            Texture2D darkeningLayer = initializeDarkeningLayerOn(currentMaterial);

            allMaterialsAndTheirDarkeningLayer.Add(currentMaterial.GetInstanceID(), darkeningLayer.GetPixels());

            map.addLightableSurface(darkeningLayer);
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

        Color[] pixelsOfDarkeningLayer;
        allMaterialsAndTheirDarkeningLayer.TryGetValue(material.GetInstanceID(), out pixelsOfDarkeningLayer);

        float coordX = hitSpot.textureCoord.x * material.mainTexture.width;
        float coordY = hitSpot.textureCoord.y * material.mainTexture.height;

        lightUpACircle(ref pixelsOfDarkeningLayer, coordX, coordY, map, material.mainTexture.width, material.mainTexture.height, ref material);
    }

    private static void lightUpACircle(ref Color[] pixels, float coordX, float coordY, Map map, int darkeningLayerWidth, int darkeningLayerHeight, ref Material material)
    {
        int numberOfTexelsChanged = 0;
        numberOfTexelsChanged = changeColor(coordX, coordY, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);

        numberOfTexelsChanged = changeColor(coordX + 2, coordY, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX + 1, coordY, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX - 1, coordY, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX - 2, coordY, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);

        numberOfTexelsChanged = changeColor(coordX, coordY + 2, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX, coordY + 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX, coordY - 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX, coordY - 2, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);

        numberOfTexelsChanged = changeColor(coordX + 1, coordY + 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX - 1, coordY + 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX - 1, coordY - 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);
        numberOfTexelsChanged = changeColor(coordX + 1, coordY - 1, ref pixels, darkeningLayerWidth, darkeningLayerHeight, numberOfTexelsChanged);

        Texture2D darkeningLayer = (Texture2D)material.GetTexture("_PointsShotInfo");
        darkeningLayer.SetPixels(pixels);
        darkeningLayer.Apply();

        map.hasLightedSurface(numberOfTexelsChanged);
    }

    private static int changeColor(float coordX, float coordY, ref Color[] pixels, int darkeningLayerWidth, int darkeningLayerHeight, int numberOfPixelsChanged)
    {
        Color transparentColor = new Color(1, 1, 1, 0);
        int index = calculatePixelIndex(darkeningLayerWidth, darkeningLayerHeight, coordX, coordY);

        //TODO can ben out of index sometimes
        if (pixels[index] != transparentColor)
        {
            pixels[index] = transparentColor;
            numberOfPixelsChanged += 1;
        }

        return numberOfPixelsChanged;
    }

    private static int calculatePixelIndex(int textureWidth, int textureHeight, float coordX, float coordY)
    {
        int index = textureWidth * (int)coordY + (int)coordX;
        return index;
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
