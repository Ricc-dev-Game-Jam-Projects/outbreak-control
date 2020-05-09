using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager instance;

    public GameObject PlanePrefab;
    public List<GameObject> PlanePool;

    public RegionBehaviour SourceRegion = null;
    public RegionBehaviour DestinationRegion = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        PlanePool = new List<GameObject>();

        RegionBehaviour[] regions = FindObjectsOfType<RegionBehaviour>();
        foreach(RegionBehaviour region in regions)
        {
            //region.region.regionClicked += () =>
            //{
            //    if(SourceRegion == null)
            //    {
            //        SourceRegion = region;
            //        return;
            //    } else
            //    {
            //        DestinationRegion = region;
            //        SendPlane(SourceRegion, DestinationRegion);
            //        SourceRegion = null;
            //        DestinationRegion = null;
            //    }
            //};
        }
    }

    public void SendPlane(RegionBehaviour region01, RegionBehaviour region02)
    {
        Plane plane = GetPlaneInstance().GetComponent<Plane>();
        plane.gameObject.SetActive(true);
        plane.Travel(region01, region02);
    }

    public GameObject GetPlaneInstance()
    {
        foreach(GameObject plane in PlanePool)
        {
            if(!plane.activeInHierarchy)
            {
                return plane;
            }
        }

        GameObject go = Instantiate(PlanePrefab);
        PlanePool.Add(go);
        return go;
    }
}
