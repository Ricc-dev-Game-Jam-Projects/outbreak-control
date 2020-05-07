using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject regionPrefab;
    public int width;
    public int height;
    public int blur;
    public Sprite regionSprite;
    [Range(0f, 1f)]
    public float seaLevel;

    private float pixelsPerUnit;
    private Map map;

    private float xRand, yRand;

    void Start()
    {
        xRand = UnityEngine.Random.Range(0, 100);
        yRand = UnityEngine.Random.Range(0, 100);
        pixelsPerUnit = regionSprite.rect.width / regionSprite.bounds.size.x;
        map = new Map(width, height);

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
    }

    void Update()
    {
        map.Sweep((region) =>
        {
            region.altitude = Mathf.PerlinNoise(xRand + region.xHex / blur, yRand + region.yHex / blur) * 0.8f +
                Mathf.PerlinNoise(xRand + region.xHex / blur / 2, yRand + region.yHex / blur * 2 / 3) * 0.2f;
            region.altitude = region.altitude > seaLevel ? region.altitude : 0;
        });
    }
}
