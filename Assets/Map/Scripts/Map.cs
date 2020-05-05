
using System;
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

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //  0 1  |  * * * *
                // 5 * 2 | * * * *
                //  4 3  |  * * * *
                Region region = grid[x, y];
                Region neighbor(int _x, int _y)
                {
                    try { return grid[_x, _y]; }
                    catch (Exception) { return null; }
                }

                Mathf.PerlinNoise(1, 1);

                region.neighborhood = y % 2 == 0
                    ? new Region[] {
                    neighbor(x, y - 1), neighbor(x + 1, y - 1), neighbor(x + 1, y),
                    neighbor(x + 1, y + 1), neighbor(x, y + 1), neighbor(x - 1, y) }
                    : new Region[] {
                    neighbor(x - 1, y - 1), neighbor(x, y - 1), neighbor(x + 1, y),
                    neighbor(x, y + 1), neighbor(x - 1, y + 1), neighbor(x - 1, y) };
            }
        }
    }
}
