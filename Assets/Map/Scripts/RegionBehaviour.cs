using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionBehaviour : MonoBehaviour
{
    [SerializeField]
    public Region region;
    public Color32 selected;
    public Color32 deselected;

    void Start()
    {
        deselected = GetComponent<SpriteRenderer>().color;
        region.setColor += () =>
        {
            GetComponent<SpriteRenderer>().color = selected;
        };
        region.clearColor = () =>
        {
            GetComponent<SpriteRenderer>().color = deselected;
        };


    }

    void Update()
    {
        byte alpha = region.altitude * 255 < 0
            ? (byte)0
            : region.altitude * 255 >= 256
            ? (byte)255 : (byte)Math.Floor(region.altitude * 255);
        GetComponent<SpriteRenderer>().color =
                new Color32(0, 0, 0, alpha);
    }

    private void OnMouseDown()
    {
        region?.OnMouseDown();
    }

    private void OnMouseOver()
    {
        region?.OnMouseOver();
    }

    private void OnMouseExit()
    {
        region?.OnMouseExit();
    }
}
