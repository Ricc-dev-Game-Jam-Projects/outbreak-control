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
        float r, g, b, a = Region.Altitude;
        switch (Region.Type)
        {
            case RegionType.Ground:
                spriteRenderer.color = Color.Lerp(altitude, GroundColor, 1 - a);
                break;
            case RegionType.Coast:
                Color coastColor = Color.Lerp(GroundColor, WaterColor, 0.5f);
                spriteRenderer.color = Color.Lerp(altitude, coastColor, 1 - a);
                //r = (GroundColor.r + WaterColor.r) / 2;
                //g = (GroundColor.g + WaterColor.g) / 2;
                //b = (GroundColor.b + WaterColor.b) / 2;
                //a = 1 - a;
                break;
            case RegionType.Water:
                spriteRenderer.color = Color.Lerp(altitude, WaterColor, a);
                //r = WaterColor.r;
                //g = WaterColor.g;
                //b = WaterColor.b;
                break;
        }


        GetComponentInChildren<DelimiterHandlerBehaviour>().SetDelimiter(Region);
        GetComponentInChildren<PopulationBehaviour>().
            SetPopulation(Region.PopulationDensity);

        //GetComponent<SpriteRenderer>().color = new Color(r * a, g * a, b * a, 1);
    }

    void Update() { }
}
