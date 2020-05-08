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

        GenerateNewMap();
        DistributePopulation();
    }

    void Update() { }

    private void GenerateNewMap()
    {
        XOffset = UnityEngine.Random.Range(0, 255);
        YOffset = UnityEngine.Random.Range(0, 255);
        float xNoise, yNoise;

        map.Sweep((region) =>
        {
            xNoise = XOffset + region.XHex * Scale;
            yNoise = YOffset + region.YHex * Scale;

            region.Altitude =
                Mathf.PerlinNoise(xNoise, yNoise) * 0.5f +
                Mathf.PerlinNoise(xNoise / 3, yNoise / 3) * 0.5f;

            region.Type = region.Altitude <= SeaLevel ?
                RegionType.Water : RegionType.Ground;
        });

        map.Sweep((region) =>
        {
            if (region.Type == RegionType.Ground)
            {
                foreach (Region neighbor in region.Neighborhood)
                {
                    if (neighbor != null && neighbor.Type == RegionType.Water)
                    {
                        region.Type = RegionType.Coast;
                        break;
                    }
                }
            }
        });
    }

    private void DistributePopulation()
    {
        XOffset = UnityEngine.Random.Range(0, 255);
        YOffset = UnityEngine.Random.Range(0, 255);
        float xNoise, yNoise;

        map.Sweep((region) =>
        {
            if (region.Type != RegionType.Water)
            {
                xNoise = XOffset + region.XHex * Scale;
                yNoise = YOffset + region.YHex * Scale;

                Region nearestWater = region;
                map.BFS((_region) =>
                {
                    if (_region.Type == RegionType.Water)
                    {
                        nearestWater = _region;
                        return true;
                    }
                    return false;
                });

                float distance = Map.DistanceBetween(region, nearestWater);
                float x = Mathf.PerlinNoise(xNoise, yNoise) *
                    (16 * (1 / distance + (1 - region.Altitude)) - 10);
                region.PopulationDensity = Sigmoid(x);
            }
        });
    }

    private float Sigmoid(float x) =>
        1 / (1 + (float)Math.Pow(Math.E, -x));

}
