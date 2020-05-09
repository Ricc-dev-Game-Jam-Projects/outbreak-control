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

    public RegionType Type;
    public int District;
    public int Level;
    public float PopulationDensity;
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
    }
}