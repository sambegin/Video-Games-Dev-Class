using System;
using UnityEngine;

internal class TargetLightSwitcher
{
    public TargetLightSwitcher()
    {
    }

    internal void isHit(Renderer targetRenderer, Material newHitMaterial)
    {
        if(targetRenderer != null)
        {
            targetRenderer.material = newHitMaterial;
        }
    }
}