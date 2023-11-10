using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    private void Start()
    {
        DestructibleCrate.OnAnyDestroy += DestructibleCrate_OnAnyDestroy;
    }


    private void DestructibleCrate_OnAnyDestroy(object sender,EventArgs e)
    {
        DestructibleCrate destructibleCrate= sender as DestructibleCrate;
        Pathfinding.Instance.SetIsWalkableGridPosition(destructibleCrate.GetGridPosition(),true);
    }
}
