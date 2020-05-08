
using System;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Width;
    public int Height;
    public Region[,] Grid;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new Region[width, height];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Grid[x, y] = new Region(this, x, y);
            }
        }

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

    public void Sweep(Action<Region> action)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                action(Grid[x, y]);
            }
        }
    }
    public void Sweep(Func<Region, bool> function)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if(function(Grid[x, y])) return;
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
            {
                if(neighbor != null && !visited[neighbor.X, neighbor.Y])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.X, neighbor.Y] = true;
                }
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

            if(function(region)) return;

            foreach (Region neighbor in region.Neighborhood)
            {
                if (neighbor != null && !visited[neighbor.X, neighbor.Y])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor.X, neighbor.Y] = true;
                }
            }
        }
    }

    public static float DistanceBetween(Region region1, Region region2)
    {
        Vector2 vector1 = new Vector2(region1.XHex, region1.YHex);
        Vector2 vector2 = new Vector2(region2.XHex, region2.YHex);

        return (vector1 - vector2).magnitude;
    }
}
