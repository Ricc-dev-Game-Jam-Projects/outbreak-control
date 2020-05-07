using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject regionPrefab;

    public int wGrid, hGrid;
    [Range(0, 255)]
    public float xOffset, yOffset;
    [Range(0f, 1f)]
    public float scale;
    [Range(0f, 1f)]
    public float seaLevel;

    private float pixelsPerUnit;
    private Map map;
    private

    void Start()
    {
        
        Sprite sprite = regionPrefab.GetComponent<SpriteRenderer>().sprite;
        pixelsPerUnit = sprite.rect.width / sprite.bounds.size.x;

        map = new Map(wGrid, hGrid);

        map.Sweep((region) =>
        {
            GameObject regionGameObject = Instantiate(regionPrefab, transform, false);
            regionGameObject.GetComponent<RegionBehaviour>().region = region;

            float regionSpriteWidth =
                regionGameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
            float regionSpriteHeight =
                regionGameObject.GetComponent<SpriteRenderer>().sprite.rect.height;

            regionGameObject.transform.Translate(
                regionSpriteWidth * region.xHex / pixelsPerUnit,
                regionSpriteHeight * region.yHex / pixelsPerUnit, 0);
        });

        GenerateNewMap();
    }

    void Update()
    {
        
    }

    private void GenerateNewMap()
    {
        xOffset = UnityEngine.Random.Range(0, 255);
        yOffset = UnityEngine.Random.Range(0, 255);
        float xNoise, yNoise;

        map.Sweep((region) =>
        {
            xNoise = xOffset + region.xHex * scale;
            yNoise = yOffset + region.yHex * scale;

            region.Altitude =
                Mathf.PerlinNoise(xNoise, yNoise) * 0.5f +
                Mathf.PerlinNoise(xNoise / 3, yNoise / 3) * 0.5f;

            region.type = region.Altitude <= seaLevel ?
                RegionType.Water : RegionType.Ground;
        });
    }
}
