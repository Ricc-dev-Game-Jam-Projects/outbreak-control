using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : InteractIt
{
    public Region RegionBlock01;
    public int Position;
    public bool Builded;

    public void SetRegions(Region R01, int pos)
    {
        RegionBlock01 = R01;
        Position = pos;
        Builded = true;
    }

    public override void InteractRMB()
    {
        if (!Builded) return;
        RegionBlock01.UnblockNeighborhood(Position);
        Destroy(gameObject);
    }
}
