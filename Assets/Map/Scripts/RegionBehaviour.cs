using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    [SerializeField]
    public Color GroundColor;
    public Color WaterColor;

    public Region Region;

    void Start()
    {
        Color altitude = new Color(0,0,0,1);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (Region.Type)
        {
            case RegionType.Ground:
                spriteRenderer.color = Color.Lerp(altitude, GroundColor, 1 - a);
                break;
            case RegionType.Coast:
                Color coastColor = Color.Lerp(GroundColor, WaterColor, 0.5f);
                spriteRenderer.color = Color.Lerp(altitude, coastColor, 1 - a);
                break;
            case RegionType.Water:
                spriteRenderer.color = Color.Lerp(altitude, WaterColor, a);
                break;
        }


        GetComponentInChildren<DelimiterHandlerBehaviour>().SetDelimiter(Region);
        GetComponentInChildren<PopulationBehaviour>().
            SetPopulation(Region.PopulationDensity);
    }

    void Update() { }
}
