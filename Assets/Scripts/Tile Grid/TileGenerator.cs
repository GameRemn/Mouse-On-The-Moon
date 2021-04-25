using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public Tile tilePrefab;

    public Tile GetTile(Vector3 position)
    {
        Tile newTile;
        if (tilePrefab)
        {
            newTile = Instantiate(tilePrefab, transform, false);
            newTile.transform.localPosition = position;
        }
        else
        {
            newTile = CreateTile(position);
        }
        return newTile;
    }
    public Tile CreateTile(Vector3 position)
    {
        var newTileGameObject = new GameObject("Tile");
        var newTile = newTileGameObject.AddComponent<Tile>();
        newTileGameObject.transform.SetParent(transform, false);
        newTileGameObject.transform.localPosition = position;
        return newTile;
    }
}
