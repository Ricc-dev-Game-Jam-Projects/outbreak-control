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
        float r, g, b, a = Region.Altitude;
        switch (Region.Type)
        {
            case RegionType.Ground:
                r = GroundColor.r;
                g = GroundColor.g;
                b = GroundColor.b;
                a = 1 - a;
                break;
            case RegionType.Coast:
                r = (GroundColor.r + WaterColor.r) / 2;
                g = (GroundColor.g + WaterColor.g) / 2;
                b = (GroundColor.b + WaterColor.b) / 2;
                a = 1 - a;
                break;
            case RegionType.Water:
                r = WaterColor.r;
                g = WaterColor.g;
                b = WaterColor.b;
                break;
            default:
                r = g = b = 0;
                break;
        }

        GetComponentInChildren<DelimiterHandlerBehaviour>().SetDelimiter(Region);
        GetComponentInChildren<PopulationBehaviour>().
            SetPopulation(Region.PopulationDensity);

        GetComponent<SpriteRenderer>().color = new Color(r * a, g * a, b * a, 1);
    }

    void Update() { }
}
