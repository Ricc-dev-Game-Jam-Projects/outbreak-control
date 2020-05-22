
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
  public int Width;
  public int Height;
  public Region[,] Grid;
  public (float x, float y) offset {
    get => (UnityEngine.Random.Range(0, 255), UnityEngine.Random.Range(0, 255));
  }
  public Dictionary<RegionType, List<Region>> RegionCollections;

  public Map(int width, int height)
  {
    Width = width;
    Height = height;
    Grid = new Region[width, height];
    RegionCollections = new Dictionary<RegionType, List<Region>>()
        {
            { RegionType.Coast, new List<Region>() },
            { RegionType.Ground, new List<Region>() },
            { RegionType.Water, new List<Region>() }
        };

    InitializeGrid();
  }

  void InitializeGrid()
  {
    for (int x = 0; x < Width; x++)
      for (int y = 0; y < Height; y++)
        Grid[x, y] = new Region(this, x, y);

    ForeachRegion((region, x, y) =>
    {
      //  4 3  |  * * * *
      // 5 * 2 | * * * *
      //  0 1  |  * * * *
      Region neighbor(int _x, int _y)
      {
        try { return Grid[_x, _y]; }
        catch (Exception) { return null; }
      }

      region.Frontiers = y % 2 == 0
        ? new (Region neighbor, Wall wall)[] {
          (neighbor(x, y - 1), null), (neighbor(x + 1, y - 1), null),
          (neighbor(x + 1, y), null), (neighbor(x + 1, y + 1), null),
          (neighbor(x, y + 1), null), (neighbor(x - 1, y), null) }
        : new (Region neighbor, Wall wall)[] {
          (neighbor(x - 1, y - 1), null), (neighbor(x, y - 1), null),
          (neighbor(x + 1, y), null), (neighbor(x, y + 1), null),
          (neighbor(x - 1, y + 1), null), (neighbor(x - 1, y), null) };
    });
  }

  public void Terraform(float scale, float seaLevel)
  {
    float xNoise, yNoise;
    var offset = this.offset;

    // define relevo
    ForeachRegion((region, x, y) =>
    {
      xNoise = offset.x + region.XHex * scale;
      yNoise = offset.y + region.YHex * scale;

      float normalize = new Vector2(Width / 2, Height / 2).magnitude;
      float _x = new Vector2(x - Width / 2, y - Height / 2).magnitude;

      float island = 1 - Sigmoid(12 * _x / normalize - 6f) * 0.5f;
      float acuteRelief = Mathf.PerlinNoise(xNoise, yNoise);
      float obtuseRelief = Mathf.PerlinNoise(xNoise / 3, yNoise / 3);

      region.Altitude = island * (acuteRelief + obtuseRelief) / 2;

      if (region.Altitude > seaLevel && (y <= 0 || y >= Height - 1))
        region.Altitude = seaLevel;

      region.Type = region.Altitude <= seaLevel ?
        RegionType.Water : RegionType.Ground;
    });

    // define os Coast
    ForeachRegion((region) =>
    {
      if (region.IsGround())
        foreach (var (neighbor, wall) in region.Frontiers)
          if (neighbor != null && neighbor.IsWater())
          {
            region.Type = RegionType.Coast;
            break;
          }

      RegionCollections[region.Type].Add(region);
    });
  }

  public void DefineRivers(float occurrence)
  {
    foreach (Region coast in RegionCollections[RegionType.Coast])
    {
      if (UnityEngine.Random.Range(0f, 1f) > occurrence) continue;

      var below = coast.GetLowerNeighbor();
      var above = coast.GetHigherNeighbor();
      Region currentRegion = coast;
      Region previousRegion = coast.Frontiers[below.i].neighbor;

      do
      {
        currentRegion.ForeachNeighbor((neighbor, i) =>
        {
          if (neighbor == previousRegion) below = (neighbor, i);
        });
        above = currentRegion.GetHigherNeighbor();

        currentRegion.River.exists = true;
        currentRegion.River.pairs.Add((below.i, above.i));

        previousRegion = currentRegion;
        if (above.i != -1)
          currentRegion = currentRegion.Frontiers[above.i].neighbor;
      } while (above.i != -1);
    }
  }

  public void DistributePopulation(float scale)
  {
    float xNoise, yNoise;
    var offset = this.offset;

    ForeachRegion((region) =>
    {
      if (region.Type != RegionType.Water)
      {
        xNoise = offset.x + region.XHex * scale;
        yNoise = offset.y + region.YHex * scale;

        float distance = DistanceFromWater(region);
        float x = 2 * 1 / distance + 5 * (1 - region.Altitude) - 2;

        Culture culture = new Culture("Essa é uma cultura");
        culture.GenerateCulture(region);

        float population = Mathf.PerlinNoise(xNoise, yNoise) * Sigmoid(x);

        region.city = new City(population * Population.Max, region, culture);

        //Debug.Log(population * Population.Max);

        //region.ForeachNeighbor((neighbor, i) =>
        //{
        //  if (neighbor != null && neighbor.Type != RegionType.Water)
        //    region.city.economy.ConnectEconomy(neighbor.city.economy);
        //});
      }
    });
  }

  public void UpdatePerWeek()
  {
    float[,,] migrations = new float[Width, Height, 6];

    ForeachRegion((region, x, y) =>
    {
      if (!region.IsWater())
      {
        region.ForeachNeighbor((neighbor, wall, i) =>
        {
          if (!neighbor.IsWater())
          {
            migrations[x, y, i] =
              City.MigrationIntensity(region.city, neighbor.city) / 6;

            if (migrations[x, y, i] < 0) migrations[x, y, i] = 0;

            if (wall != null && wall.Resistance >= migrations[x, y, i])
              migrations[x, y, i] = 0;
            else region.UnblockNeighborhood(i);
          }
        });
      }
    });

    ForeachRegion((region, x, y) =>
    {
      if (!region.IsWater())
      {
        float totalMigration = 0;
        for (int i = 0; i < 6; i++) totalMigration += migrations[x, y, i];

        region.ForeachNeighbor((neighbor, i) =>
        {
          if (!neighbor.IsWater())
          {
            float limitMax = 1 - neighbor.city.population.Density;
            Population migrate = region.city.population * migrations[x, y, i];
            region.city.population -= migrate / totalMigration * limitMax;
            neighbor.city.population += migrate / totalMigration * limitMax;
          }
        });
      }
    });

    ForeachRegion((region, x, y) =>
    {
      if (!region.IsWater()) region.city.UpdatePerWeek();
    });
  }

  public void UpdatePerDay(Virus virus)
  {
    ForeachRegion((region) =>
    {
      if (!region.IsWater()) region.city.UpdatePerDay(virus);
    });
  }

  public void StartInfection()
  {
    int x, y;

    do
    {
      x = UnityEngine.Random.Range(0, Width);
      y = UnityEngine.Random.Range(0, Height);
    } while (Grid[x, y].IsWater() || Grid[x, y].city == null);

    Grid[x, y].city.population.Asymptomatic.Clear();
    Grid[x, y].city.population.Asymptomatic.Enqueue(Population.p);
  }

  public float DistanceFromWater(Region first)
  {
    Region nearestWater = first;
    BFS(first, (region) =>
    {
      if (region.IsWater())
      {
        nearestWater = region;
        return true;
      }
      return false;
    });

    return DistanceBetween(first, nearestWater);
  }

  public void ForeachRegion(Action<Region> action)
  {
    for (int x = 0; x < Width; x++)
      for (int y = 0; y < Height; y++)
        action(Grid[x, y]);
  }
  public void ForeachRegion(Action<Region, int, int> action)
  {
    for (int x = 0; x < Width; x++)
      for (int y = 0; y < Height; y++)
        action(Grid[x, y], x, y);
  }
  public void ForeachRegion(Func<Region, bool> function)
  {
    for (int x = 0; x < Width; x++)
      for (int y = 0; y < Height; y++)
        if (function(Grid[x, y])) return;
  }

  public void BFS(Region first, Action<Region> action)
  {
    bool[,] visited = new bool[Width, Height];
    Queue<Region> queue = new Queue<Region>();

    visited[first.X, first.Y] = true;
    queue.Enqueue(first);

    while (queue.Count != 0)
    {
      Region region = queue.Dequeue();

      action(region);

      foreach (var (neighbor, wall) in region.Frontiers)
        if (neighbor != null && !visited[neighbor.X, neighbor.Y])
        {
          queue.Enqueue(neighbor);
          visited[neighbor.X, neighbor.Y] = true;
        }
    }
  }
  public void BFS(Action<Region> action)
  {
    bool[,] visited = new bool[Width, Height];
    Queue<Region> queue = new Queue<Region>();

    visited[0, 0] = true;
    queue.Enqueue(Grid[0, 0]);

    while (queue.Count != 0)
    {
      Region region = queue.Dequeue();

      action(region);

      foreach (var (neighbor, wall) in region.Frontiers)
        if (neighbor != null && !visited[neighbor.X, neighbor.Y])
        {
          queue.Enqueue(neighbor);
          visited[neighbor.X, neighbor.Y] = true;
        }
    }
  }
  public void BFS(Region first, Func<Region, bool> function)
  {
    bool[,] visited = new bool[Width, Height];
    Queue<Region> queue = new Queue<Region>();

    visited[first.X, first.Y] = true;
    queue.Enqueue(first);

    while (queue.Count != 0)
    {
      Region region = queue.Dequeue();

      if (function(region)) return;

      foreach (var (neighbor, wall) in region.Frontiers)
        if (neighbor != null && !visited[neighbor.X, neighbor.Y])
        {
          queue.Enqueue(neighbor);
          visited[neighbor.X, neighbor.Y] = true;
        }
    }
  }
  public void BFS(Func<Region, bool> function)
  {
    bool[,] visited = new bool[Width, Height];
    Queue<Region> queue = new Queue<Region>();

    visited[0, 0] = true;
    queue.Enqueue(Grid[0, 0]);

    while (queue.Count != 0)
    {
      Region region = queue.Dequeue();

      if (function(region)) return;

      foreach (var (neighbor, wall) in region.Frontiers)
        if (neighbor != null && !visited[neighbor.X, neighbor.Y])
        {
          queue.Enqueue(neighbor);
          visited[neighbor.X, neighbor.Y] = true;
        }
    }
  }

  public static float DistanceBetween(Region region1, Region region2)
  {
    Vector2 vector1 = new Vector2(region1.XHex, region1.YHex);
    Vector2 vector2 = new Vector2(region2.XHex, region2.YHex);

    return (vector1 - vector2).magnitude;
  }

  private static float Sigmoid(float x) =>
    1 / (1 + (float)Math.Pow(Math.E, -x));

  private static float Arc(float x) =>
    (float)Math.Sin(Math.Atan(x));
}
