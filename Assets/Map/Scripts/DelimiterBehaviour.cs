using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelimiterBehaviour : MonoBehaviour
{
    public GameObject delimiterPrefab;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDelimiter(Region region)
    {
        for (int i = 0; i < region.neighborhood.Length; i++)
        {
            if (region.neighborhood[i] != null &&
                region.neighborhood[i].level < region.level)
            {
                GameObject delimiter = Instantiate(delimiterPrefab, transform, false);
                delimiter.transform.rotation = Quaternion.Euler(0, 0, 60 * i + 120);
                delimiter.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
                delimiter.GetComponent<SpriteRenderer>().sprite = sprite;
            }

        }
    }
}
