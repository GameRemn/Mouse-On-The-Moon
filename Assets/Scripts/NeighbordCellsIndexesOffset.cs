using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NeighbordCellsIndexesOffset
{
    public static List<Vector3Int> XZCross = new List<Vector3Int>()
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(-1, 0,0),
        new Vector3Int(0, 0, -1),
        new Vector3Int(1,0,0)
    };
}
