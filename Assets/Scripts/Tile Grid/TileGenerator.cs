using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour
{
    public List<TileChance> tileChances;

    public Tile GetTile(Vector3 position)
    {
        Tile newTile; 
        var tilePrefab = SelectTileByChance(tileChances);
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

    public static Tile SelectTileByChance(List<TileChance> tileChances)
    {
        int summaryChance = 0;
        foreach (var tileChance in tileChances)
        {
            summaryChance += tileChance.chance;
        }
        var elementChance = Random.Range(0, summaryChance);
        foreach (var tileChance in tileChances)
        {
            if (elementChance < tileChance.chance)
            {
                return tileChance.tile;
            }
            else
            {
                elementChance -= tileChance.chance;
            }
        }
        return null;
    }
}
[System.Serializable]
public class TileChance
{
    public Tile tile;
    public int chance;
}
