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
        region.setColor = () =>
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

    }

    private void OnMouseDown()
    {
        region?.OnMouseDown();
    }

    private void OnMouseOver()
    {
        foreach (var neighbor in region.neighborhood)
        {
            neighbor?.OnMouseOver();
        }
    }

    private void OnMouseExit()
    {
        foreach (var neighbor in region.neighborhood)
        {
            neighbor?.OnMouseExit();
        }
    }
}
