using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpBehaviour : MonoBehaviour, IBehaviour
{
    public static TrumpBehaviour Trump;

    public GameObject WallPrefab;
    public GameObject CurrentWall;

    public bool IsBuildingWalls;

    private int WallPosition = 0;

    public void MyAwake()
    {
        if(Trump == null)
        {
            Trump = this;
        } else
        {
            DestroyImmediate(Trump);
        }
    }

    public void MyStart()
    {
        GetNewWall();
    }

    void Update()
    {
        if (IsBuildingWalls && RegionBehaviour.RegionLooking != null)
        {
            RegionBehaviour regionBeha = RegionBehaviour.RegionLooking;
            CurrentWall.SetActive(true);
            CurrentWall.transform.position = regionBeha.transform.position;

            if (Input.GetMouseButtonUp(0))
            {
                if(regionBeha.BuildWall(WallPosition))
                {
                    CurrentWall.transform.SetParent(regionBeha.transform);
                    CurrentWall.GetComponent<LiteralWall>().SetRegions(regionBeha.Region, WallPosition);
                    GetNewWall();
                } else
                {
                    Debug.Log("Already exist a wall here, Trump... go slowly");
                }
            }
            int val = 0;
            if (Input.GetKeyUp(KeyCode.E))
            {
                val = 1;
            } else if (Input.GetKeyUp(KeyCode.Q))
            {
                val = -1;
            }
            WallPosition += val;
            if(WallPosition > 5)
            {
                WallPosition = 0;
            } else if(WallPosition < 0)
            {
                WallPosition = 5;
            }
            CurrentWall.transform.rotation = Quaternion.Euler(0, 0, 60*WallPosition);
        } else
        {
            WallPosition = 0;
            CurrentWall.transform.rotation = Quaternion.Euler(0, 0, 120f);
            CurrentWall.SetActive(false);
        }
    }

    public void ToggleBuilding()
    {
        IsBuildingWalls = !IsBuildingWalls;
    }

    public void GetNewWall()
    {
        CurrentWall = Instantiate(WallPrefab);
        CurrentWall.SetActive(false);
    }
}
