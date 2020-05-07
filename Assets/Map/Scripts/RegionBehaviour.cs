using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    [SerializeField]
    public Region region;
    public Color groundColor;
    public Color waterColor;

    void Start()
    {
        float r, g, b, a = region.Altitude;
        switch (region.type)
        {
            case RegionType.Ground:
                r = groundColor.r;
                g = groundColor.g;
                b = groundColor.b;
                a = 1 - a;
                break;
            case RegionType.Water:
                r = waterColor.r;
                g = waterColor.g;
                b = waterColor.b;
                break;
            default:
                r = g = b = 0;
                break;
        }

        GetComponentInChildren<DelimiterBehaviour>().SetDelimiter(region);

        GetComponent<SpriteRenderer>().color = new Color(r * a, g * a, b * a, 1);
    }

    void Update()
    {

    }

    //private void OnMouseDown()
    //{
    //    region?.OnMouseDown();
    //}

    //private void OnMouseOver()
    //{
    //    region?.OnMouseOver();
    //}

    //private void OnMouseExit()
    //{
    //    region?.OnMouseExit();
    //}
}
