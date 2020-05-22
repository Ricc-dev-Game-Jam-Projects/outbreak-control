using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour, IBehaviour
{
    public GameObject RegionPrefab;
    public GameObject RegionBlocked;
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

    public Map map;

    public void MyAwake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void MyStart()
    {
        Sprite sprite = RegionPrefab.GetComponent<SpriteRenderer>().sprite;
        pixelsPerUnit = sprite.rect.width / sprite.bounds.size.x;
        map = new Map(WGrid, HGrid);

        map.ForeachRegion((region) =>
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

            regionGameObject.name = region.XHex + " " + region.YHex;
        });

        map.Terraform(Scale, SeaLevel);
        map.DefineRivers(RiverOccurrence);
        map.DistributePopulation(Scale);

        foreach(Transform t in transform)
        {
            t.GetComponent<RegionBehaviour>().Initialize();
        }

        map.StartInfection();
        StartCoroutine("CoolDownToInfect");
    }

    IEnumerator CoolDownToInfect()
    {
        yield return new WaitForSecondsRealtime(5f);
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
        foreach (Transform t in gameObject.transform)
        {
            t.GetComponent<RegionBehaviour>().ShowPopulation(show);
        }
    }
}
