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
  public (Region neighbor, Wall wall)[] Frontiers;
  //public Region[] Blocked;

  public City city;
  public RegionType Type;
  public (bool exists, List<(int below, int above)> pairs) River;
  public int Level;

  public Action RegionInfected;

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
    Frontiers = new (Region neighbor, Wall block)[6];
    //Blocked = new Region[6];
    River = (false, pairs: new List<(int below, int above)>());
  }

  public bool IsWater() => Type == RegionType.Water;
  public bool IsCoast() => Type == RegionType.Coast;
  public bool IsGround() => Type == RegionType.Ground;

  public void BlockNeighborhood(int neigh)
  {
    Wall wall = new Wall(0.5f);

    Frontiers[neigh].wall = wall;
    Frontiers[neigh].neighbor.Frontiers[(neigh + 3) % 6].wall = wall;
  }

  public void UnblockNeighborhood(int neigh)
  {
    Frontiers[neigh].wall = null;
    Frontiers[neigh].neighbor.Frontiers[(neigh + 3) % 6].wall = null;
  }

  public void ForeachNeighbor(Action<Region, int> action)
  {
    for (int i = 0; i < Frontiers.Length; i++)
      if(Frontiers[i].neighbor != null)
        action(Frontiers[i].neighbor, i);
  }

  public void ForeachNeighbor(Action<Region, Wall, int> action)
  {
    for (int i = 0; i < Frontiers.Length; i++)
      if (Frontiers[i].neighbor != null)
        action(Frontiers[i].neighbor, Frontiers[i].wall, i);
  }

  //public void ForeachFreeNeighbor(Action<Region, int> action)
  //{
  //  for (int i = 0; i < Neighborhood.Length; i++)
  //  {
  //    if (Blocked[i] == null)
  //    {
  //      action(Neighborhood[i], i);
  //    }
  //  }
  //}

  public (Region neighbor, int i) GetLowerNeighbor()
  {
    (Region neighbor, int i) pair = (this, -1);
    ForeachNeighbor((neighbor, i) =>
    {
      if (neighbor.altitude < pair.neighbor.altitude)
        pair = (neighbor, i);
    });
    return pair;
  }

  public (Region neighbor, int i) GetHigherNeighbor()
  {
    (Region neighbor, int i) pair = (this, -1);
    ForeachNeighbor((neighbor, i) =>
    {
      if (neighbor.altitude > pair.neighbor.altitude)
        pair = (neighbor, i);
    });
    return pair;
  }

  public void OnRegionInfected()
  {
    RegionInfected?.Invoke();
  }
}