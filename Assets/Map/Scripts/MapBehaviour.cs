using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public GameObject regionPrefab;
    public int width;
    public int height;
    public int pixelsPerUnit = 100;

    private Map map;

    void Start()
    {
        map = new Map(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject regionGameObject = Instantiate(regionPrefab, this.transform, false);
                regionGameObject.GetComponent<RegionBehaviour>().region = map.grid[x, y];

                float regionSpriteWidth =
                    regionGameObject.GetComponent<SpriteRenderer>().sprite.rect.width;
                float regionSpriteHeight =
                    regionGameObject.GetComponent<SpriteRenderer>().sprite.rect.height;

                regionGameObject.transform.Translate(
                    (regionSpriteWidth * x - y % 2 * regionSpriteWidth / 2) / pixelsPerUnit,
                    regionSpriteHeight * 3 * y / 4 / pixelsPerUnit, 0);
            }
        }
    }

    void Update()
    {

    }
}
