using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarginHandlerBehaviour : MonoBehaviour
{
    public GameObject MarginPrefab;
    public Sprite[] MarginSprite;

    public void SetMargin(Region region, Color waterColor)
    {
        int waterSequence = 0;
        for (int i = 0; i < region.Neighborhood.Length; i++)
        {
            Region currentNeighbor = region.Neighborhood[i];

            if (currentNeighbor != null)
            {
                if (currentNeighbor.Type == RegionType.Water)
                    waterSequence++;
                else if (waterSequence > 0)
                {
                    GameObject margin =
                    Instantiate(MarginPrefab, transform, false);

                    margin.transform.rotation =
                        Quaternion.Euler(0, 0, 60 * i + 60);
                    SpriteRenderer spriteRenderer =
                        margin.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = waterColor;
                    spriteRenderer.sprite = MarginSprite[waterSequence - 1];
                    spriteRenderer.sortingOrder = 2;

                    waterSequence = 0;
                }
            } else waterSequence++;
        }
        if(waterSequence > 0)
        {
            GameObject margin =
                    Instantiate(MarginPrefab, transform, false);

            margin.transform.rotation =
                Quaternion.Euler(0, 0, 60 * 6 + 60);
            SpriteRenderer spriteRenderer =
                margin.GetComponent<SpriteRenderer>();
            spriteRenderer.color = waterColor;
            spriteRenderer.sprite = MarginSprite[waterSequence - 1];
            spriteRenderer.sortingOrder = 2;
        }
    }
}
