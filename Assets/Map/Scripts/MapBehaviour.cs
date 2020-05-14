﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject RegionPrefab;
    public static MapBehaviour instance;

    public int WGrid, HGrid;
    [Range(0, 255)]
    public float XOffset, YOffset;
    [Range(0f, 1f)]
    public float Scale;
    [Range(0f, 1f)]
    public float SeaLevel;
    [Range(0f, 1f)]
    public float RiverOccurrence;

    private float pixelsPerUnit;
    private Map map;

    public Map _map { get { return map; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Sprite sprite = RegionPrefab.GetComponent<SpriteRenderer>().sprite;
        pixelsPerUnit = sprite.rect.width / sprite.bounds.size.x;
        map = new Map(WGrid, HGrid);

        map.Sweep((region) =>
        {
            GameObject regionGameObject =
                Instantiate(RegionPrefab, transform, false);
            regionGameObject.GetComponent<RegionBehaviour>().Region = region;

            SpriteRenderer spriteRenderer =
                regionGameObject.GetComponent<SpriteRenderer>();
            float regionSpriteWidth = spriteRenderer.sprite.rect.width;
            float regionSpriteHeight = spriteRenderer.sprite.rect.height;

            regionGameObject.transform.Translate(
                regionSpriteWidth * region.XHex / pixelsPerUnit,
                regionSpriteHeight * region.YHex / pixelsPerUnit, 0);
        });

        map.GenerateNewMap(Scale, SeaLevel);
        map.DistributePopulation(Scale);
        map.DefineRivers(RiverOccurrence);
        map.StartInfection();
    }

    public void UpdateRegions()
    {
        foreach (Transform t in gameObject.transform)
        {
            t.GetComponent<RegionBehaviour>().UpdateRegion();
        }
    }

    public void ShowPopulation(bool show)
    {
        foreach(Transform t in gameObject.transform)
        {
            t.GetComponent<RegionBehaviour>().ShowPopulation(show);
        }
    }
}
