
using System;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int width;
    public int height;
    public Region[,] grid;

    public Map(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Region[width, height];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Region(this, x, y);
            }
        }

        Sweep((region) =>
        {
            //  0 1  |  * * * *
            // 5 * 2 | * * * *
            //  4 3  |  * * * *
            Region neighbor(int _x, int _y)
            {
                try { return grid[_x, _y]; }
                catch (Exception) { return null; }
            }

            Mathf.PerlinNoise(1, 1);
            int x = region.x;
            int y = region.y;

            region.neighborhood = y % 2 == 0
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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                action(grid[x, y]);
            }
        }
    }

    public void BFS(Action<Region> action)
    {
        bool[,] visited = new bool[width, height];
        Queue<Region> queue = new Queue<Region>();

        visited[0, 0] = true;
        queue.Enqueue(grid[0, 0]);
        
        while (queue.Count != 0)
        {
            Region region = queue.Dequeue();
            foreach(Region neighbor in region.neighborhood)
            {
                if(!visited[neighbor.x, neighbor.y])
                {
                    action(neighbor);

                    queue.Enqueue(neighbor);
                    visited[neighbor.x, neighbor.y] = true;
                }
            }
        }
    }
}
