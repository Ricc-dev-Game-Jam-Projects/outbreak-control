using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Region
{
    public Map map;
    public Vector2 position;
    public Region[] neighborhood;

    public UnityAction setColor;
    public UnityAction clearColor;

    public Region(Map map, int x, int y)
    {
        this.map = map;
        position = new Vector2(x, y);
        neighborhood = new Region[6];
    }

    public Region(Map map, Vector2 position)
    {
        this.map = map;
        this.position = position;
        neighborhood = new Region[6];
    }

    public void OnMouseOver()
    {
        setColor?.Invoke();
    }

    public void OnMouseExit()
    {
        clearColor?.Invoke();
    }
}
