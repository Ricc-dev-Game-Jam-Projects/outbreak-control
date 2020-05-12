using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverHandler : MonoBehaviour
{
    public GameObject RiverPrefab;
    public Sprite[] RiverSprite;
    public Sprite RiverStart;

    private int[,] index = new int[,] {
        { 0, 1, 2, 3, 2, 1 },
        { 1, 0, 1, 2, 3, 2 },
        { 2, 1, 0, 1, 2, 3 },
        { 3, 2, 1, 0, 1, 2 },
        { 2, 3, 2, 1, 0, 1 },
        { 1, 2, 3, 2, 1, 0 }
    };

    public void SetRiver(Region region, Color waterColor)
    {

        try
        {
            foreach (var r in region.River.pairs)
            {
                GameObject river = Instantiate(RiverPrefab, transform, false);
                SpriteRenderer spriteRenderer = river.GetComponent<SpriteRenderer>();
                spriteRenderer.color =
                    Color.Lerp(Color.black, waterColor, region.Altitude);
                spriteRenderer.sortingOrder = 2;

                if (r.above == -1)
                {
                    spriteRenderer.sprite = RiverStart;
                    river.transform.rotation = Quaternion.Euler(0, 0, 60 * r.below);
                }
                else
                {
                    int rotate = Math.Abs(r.below - r.above) == 2
                        ? (r.below > r.above ? r.above : r.below)
                        : (r.below < r.above ? r.above : r.below);

                    river.transform.rotation = Quaternion.Euler(0, 0, 60 * rotate);
                    spriteRenderer.sprite = RiverSprite[index[r.above, r.below]];
                }
            }
        }
        catch (Exception)
        {
            Debug.Log(index);
        }
    }
}
