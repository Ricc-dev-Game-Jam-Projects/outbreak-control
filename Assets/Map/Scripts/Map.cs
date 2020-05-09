﻿
using System;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Width;
    public int Height;
    //public Dictionary<RegionType, int> NumberRegions;
    public Region[,] Grid;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new Region[width, height];
        //NumberRegions = new Dictionary<RegionType, int>()
        //{
        //    { RegionType.Coast, 0 },
        //    { RegionType.Ground, 0 },
        //    { RegionType.Water, 0 },
        //};
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

            region.Altitude =
                Mathf.PerlinNoise(xNoise, yNoise) * 0.5f +
                Mathf.PerlinNoise(xNoise / 3, yNoise / 3) * 0.5f;

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
        });

        //Sweep((region) => NumberRegions[region.Type]++);
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
                yNoise = xOffset + region.YHex * scale;

                Region nearestWater = region;
                BFS((_region) =>
                {
                    if (_region.Type == RegionType.Water)
                    {
                        nearestWater = _region;
                        return true;
                    }
                    return false;
                });

                float distance = DistanceBetween(region, nearestWater);
                float x = Mathf.PerlinNoise(xNoise, yNoise) *
                    (16 * (1 / distance + (1 - region.Altitude)) - 10);
                region.PopulationDensity = Sigmoid(x);
            }
        });
    }

    //public void SetDistricts(int numberDistricts)
    //{
    //    int regionsPerDistrict =
    //        (NumberRegions[RegionType.Ground] +
    //        NumberRegions[RegionType.Coast]) / numberDistricts;

    //    for (int i = 1; i <= numberDistricts; i++)
    //    {
    //        int x = UnityEngine.Random.Range(0, Width);
    //        int y = UnityEngine.Random.Range(0, Height);
    //        Region startDistrict = Grid[x, y];
    //        int nRegions = regionsPerDistrict;

    //        if (startDistrict.Type == RegionType.Water ||
    //            startDistrict.District != 0) { i--; continue; }

    //        startDistrict.District = i;

    //        BFS(startDistrict, (region) =>
    //        {
    //            if (region.Type != RegionType.Water)
    //            {
    //                if (nRegions == 0) return true;
    //                if (region.District == 0)
    //                {
    //                    region.District = i;
    //                    nRegions--;
    //                }
    //            }
    //            return false;
    //        });
    //    }
    //}

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
