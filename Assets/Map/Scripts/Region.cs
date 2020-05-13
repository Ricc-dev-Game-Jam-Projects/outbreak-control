using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Region
{
    public Map Map;
    public int X, Y;
    public float XHex, YHex;
    public Region[] Neighborhood;

    public City city;
    public RegionType Type;
    public (bool exists, List<(int below, int above)> pairs) River;
    public int Level;
    private float altitude;
    public float Altitude {
        get { return altitude; }
        set {
            Level = (int)Math.Truncate(value * 10);
            altitude = value;
        }
    }

    public Region(Map map, int x, int y)
    {
        Map = map;
        X = x;
        Y = y;
        XHex = x - (float)y % 2 / 2;
        YHex = (float)3 * y / 4;
        Neighborhood = new Region[6];
        River = (false, pairs: new List<(int below, int above)>());
    }

    public void ForeachNeighbor(Action<Region, int> action)
    {
        for (int i = 0; i < Neighborhood.Length; i++)
            action(Neighborhood[i], i);
    }

    public (Region neighbor, int i) GetLowerNeighbor()
    {
        (Region neighbor, int i) pair = (this, -1);
        ForeachNeighbor((neighbor, i) =>
        {
            if (neighbor != null && neighbor.altitude < pair.neighbor.altitude)
                pair = (neighbor, i);
        });
        return pair;
    }

    public (Region neighbor, int i) GetHigherNeighbor()
    {
        (Region neighbor, int i) pair = (this, -1);
        ForeachNeighbor((neighbor, i) =>
        {
            if (neighbor != null && neighbor.altitude > pair.neighbor.altitude)
                pair = (neighbor, i);
        });
        return pair;
    }
}