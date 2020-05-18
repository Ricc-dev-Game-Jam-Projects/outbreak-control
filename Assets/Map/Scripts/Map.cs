
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map
{
    public int Width;
    public int Height;
    public Region[,] Grid;
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

        Sweep((region) =>
        {
            //  0 1  |  * * * *
            // 5 * 2 | * * * *
            //  4 3  |  * * * *
            Region neighbor(int _x, int _y)
            {
                try { return Grid[_x, _y]; }
                catch (Exception) { return null; }
            }

            Mathf.PerlinNoise(1, 1);
            int x = region.X;
            int y = region.Y;

            region.Neighborhood = y % 2 == 0
                ? new Region[] {
                    neighbor(x, y - 1), neighbor(x + 1, y - 1), neighbor(x + 1, y),
                    neighbor(x + 1, y + 1), neighbor(x, y + 1), neighbor(x - 1, y) }
                : new Region[] {
                    neighbor(x - 1, y - 1), neighbor(x, y - 1), neighbor(x + 1, y),
                    neighbor(x, y + 1), neighbor(x - 1, y + 1), neighbor(x - 1, y) };
        });
    }

    public void GenerateNewMap(float scale, float seaLevel)
    {
        float xOffset = UnityEngine.Random.Range(0, 255);
        float yOffset = UnityEngine.Random.Range(0, 255);
        float xNoise, yNoise;

        Sweep((region) =>
        {
            xNoise = xOffset + region.XHex * scale;
            yNoise = yOffset + region.YHex * scale;

            float max = new Vector2(Width / 2, Height / 2).magnitude;
            float my = new Vector2(region.X - Width / 2, region.Y - Height / 2).magnitude;
            region.Altitude = 1.1f * (1 - Sigmoid(12 * my / max - 6f)) *
                (Mathf.PerlinNoise(xNoise, yNoise) * 0.5f +
                Mathf.PerlinNoise(xNoise / 3, yNoise / 3) * 0.5f) + 0.1f;

            region.Type = region.Altitude <= seaLevel ?
                RegionType.Water : RegionType.Ground;
        });

        Sweep((region) =>
        {
            if (region.Type == RegionType.Ground)
            {
                foreach (Region neighbor in region.Neighborhood)
                {
                    if (neighbor != null && neighbor.Type == RegionType.Water)
                    {
                        region.Type = RegionType.Coast;
                        break;
                    }
                }
            }

            RegionCollections[region.Type].Add(region);
        });
    }

    public void DistributePopulation(float scale)
    {
        float xOffset = UnityEngine.Random.Range(0, 255);
        float yOffset = UnityEngine.Random.Range(0, 255);
        float xNoise, yNoise;

        Sweep((region) =>
        {
            if (region.Type != RegionType.Water)
            {
                xNoise = xOffset + region.XHex * scale;
                yNoise = yOffset + region.YHex * scale;

                Region nearestWater = region;
                BFS(region, (_region) =>
                {
                    if (_region.Type == RegionType.Water)
                    {
                        nearestWater = _region;
                        return true;
                    }
                    return false;
                });

                float distance = DistanceBetween(region, nearestWater);
                float x =
                    (2 * 1 / distance + 5 * (1 - region.Altitude) - 2);
                Culture culture = new Culture("Essa é uma cultura");
                culture.GenerateCulture(region);
                float population = Mathf.PerlinNoise(xNoise, yNoise) * Sigmoid(x);
                region.city = new City(population, region, culture);
                //region.ForeachNeighbor((neighbor, i) =>
                //{
                //    if(neighbor != null && neighbor.Type != RegionType.Water)
                //        region.city.economy.ConnectEconomy(neighbor.city.economy);
                //});
            }
        });
    }

    public void UpdatePerWeek()
    {
        (float notInfected,
            float infected,
            Queue<float> asymptomatic)[,,] migrations = new (
            float notInfected,
            float infected,
            Queue<float> asymptomatic)[Width, Height, 6];
        Sweep((region) =>
        {
            if (region.Type != RegionType.Water)
            {
                region.city.UpdatePerWeek();
                region.ForeachNeighbor((neighbor, i) =>
                {
                    if (neighbor != null &&
                        neighbor.Type != RegionType.Water &&
                        neighbor.city.Population < 1)
                        migrations[region.X, region.Y, i] =
                            City.PrepareMigrationPerDay(region.city, neighbor.city);
                });

            }
        });
        Sweep((region) =>
        {
            if (region.Type != RegionType.Water)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (region.Neighborhood[i] != null &&
                        region.Neighborhood[i].Type != RegionType.Water)
                    {
                        region.city.Emigrate(migrations[region.X, region.Y, i]);
                        region.Neighborhood[i].city.
                            Immigrate(migrations[region.X, region.Y, i]);
                    }
                }
            }
        });
    }

    public void UpdatePerDay(Virus virus)
    {
        Sweep((region) =>
        {
            if (region.Type != RegionType.Water)
                region.city.UpdatePerDay(virus);
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
            Region previousRegion = coast.Neighborhood[below.i];

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
                    currentRegion = currentRegion.Neighborhood[above.i];
            } while (above.i != -1);
        }
    }

    public void StartInfection()
    {
        int x;
        int y;

        do
        {
            x = UnityEngine.Random.Range(0, Width);
            y = UnityEngine.Random.Range(0, Height);
        } while (Grid[x, y].Type == RegionType.Water && Grid[x, y].city == null);

        Debug.Log("Region infected: " + x + ", " + y);
        Grid[x, y].city.Asymptomatic.Enqueue(City.Person);
    }

    public float DistanceFromWater(Region region)
    {
        Region nearestWater = region;
        BFS(region, (_region) =>
        {
            if (_region.Type == RegionType.Water)
            {
                nearestWater = _region;
                return true;
            }
            return false;
        });

        return DistanceBetween(region, nearestWater);
    }

    public void Sweep(Action<Region> action)
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                action(Grid[x, y]);
    }
    public void Sweep(Func<Region, bool> function)
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

            foreach (Region neighbor in region.Neighborhood)
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

            foreach (Region neighbor in region.Neighborhood)
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

            foreach (Region neighbor in region.Neighborhood)
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

            foreach (Region neighbor in region.Neighborhood)
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
}
