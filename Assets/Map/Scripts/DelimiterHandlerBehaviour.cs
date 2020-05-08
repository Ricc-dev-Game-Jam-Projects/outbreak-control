using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimiterHandlerBehaviour : MonoBehaviour
{
    public GameObject DelimiterPrefab;
    public Sprite DelimiterSprite;

    public void SetDelimiter(Region region)
    {
        for (int i = 0; i < region.Neighborhood.Length; i++)
        {
            if (region.Neighborhood[i] != null &&
                region.Neighborhood[i].Level < region.Level)
            {
                GameObject delimiter =
                    Instantiate(DelimiterPrefab, transform, false);
                delimiter.transform.rotation =
                    Quaternion.Euler(0, 0, 60 * i + 120);
                delimiter.GetComponent<SpriteRenderer>().color =
                    new Color(1, 1, 1, 0.3f);
                delimiter.GetComponent<SpriteRenderer>().sprite = DelimiterSprite;
            }
        }
    }
}
