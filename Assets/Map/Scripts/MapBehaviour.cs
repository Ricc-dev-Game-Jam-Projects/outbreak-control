using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject RegionPrefab;

    public int WGrid, HGrid;
    [Range(0, 255)]
    public float XOffset, YOffset;
    [Range(0f, 1f)]
    public float Scale;
    [Range(0f, 1f)]
    public float SeaLevel;
    [Range(1, 20)]
    public int numberDistricts;

    private float pixelsPerUnit;
    private Map map;

    void Start()
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
        map.SetDistricts(numberDistricts);
    }

    void Update() { }
}
