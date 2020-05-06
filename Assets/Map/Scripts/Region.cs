using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Region
{
    public Map map;
    public int x, y;
    public float xHex, yHex;
    public Region[] neighborhood;

    public bool color;

    public float altitude;

    public UnityAction setColor;
    public UnityAction clearColor;
    public UnityAction regionClicked;

    public Region(Map map, int x, int y)
    {
        this.map = map;
        this.x = x;
        this.y = y;
        xHex = x - (float) y % 2 / 2;
        yHex = (float) 3 * y / 4;
        neighborhood = new Region[6];
    }

    public void OnMouseDown()
    {
        regionClicked?.Invoke();
    }

    public void OnMouseOver()
    {
        foreach(Region neighbor in neighborhood)
        {
            setColor?.Invoke();
        }
    }

    public void OnMouseExit()
    {
        foreach (Region neighbor in neighborhood)
        {
            clearColor?.Invoke();
        }
    }
}
