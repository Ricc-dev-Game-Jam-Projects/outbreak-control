using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimiterHandler : MonoBehaviour
{
  public GameObject DelimiterPrefab;
  public Sprite DelimiterSprite;

  public void SetDelimiter(Region region)
  {
    region.ForeachNeighbor((neighbor, i) =>
    {
      if (neighbor != null && neighbor.Level < region.Level)
      {
        if (region.IsCoast() && neighbor.IsWater()) return;

        GameObject delimiter = Instantiate(DelimiterPrefab, transform, false);
        delimiter.transform.rotation = Quaternion.Euler(0, 0, 60 * i + 120);
        SpriteRenderer spriteRenderer = delimiter.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0.2f);
        spriteRenderer.sprite = DelimiterSprite;
        spriteRenderer.sortingOrder = 3;
      }
    });
  }
}
