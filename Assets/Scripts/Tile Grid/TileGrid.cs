using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public CellGrid cellGrid;
    public Tile tilePrefab;
    public List<Tile> tiles;

    public virtual void CreateTileGrid(List<Cell> cells)
    {
        foreach (var tilesCell in cells)
        {
            if (tilesCell.tile)
            {
                Debug.LogError("Cell hewe Tile");
                continue;
            }

            var position = tilesCell.transform.localPosition;
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

            newTile.Cell = tilesCell;
            tiles.Add(newTile);
        }
    }

    public Tile CreateTile(Vector3 position)
    {
        var newTileGameObject = new GameObject("Tile");
        var newTile = newTileGameObject.AddComponent<Tile>();
        newTileGameObject.transform.SetParent(transform, false);
        newTileGameObject.transform.localPosition = position;
        return newTile;
    }

    public bool DestroyTiles(List<Tile> tileList)
    {
        if (!tiles.Any(tileList.Contains)) return false;
        tiles.RemoveAll(tileList.Contains);
        foreach (var tile in tileList)
        {
            Destroy(tile.gameObject);
        }

        return true;
    }

    public void DestroyTile(Tile tile)
    {
        if (tiles.Contains(tile))
        {
            tiles.Remove(tile);
        }
        Destroy(tile.gameObject);
    }

    public List<Tile> NeighbordTiles(Cell cell, List<Vector3Int> neighbordCellsIndexesOffset)
    {
        List<Tile> neighbordTiles = new List<Tile>();
        var neighbordCells = cellGrid.NeighbordCells(cell, neighbordCellsIndexesOffset);
        foreach (var neighboringCell in neighbordCells)
        {
            if (neighboringCell.tile != null)
            {
                neighbordTiles.Add(cell.tile);
            }
        }

        return neighbordTiles;
    }

    public List<Tile> NeighbordTiles(Tile tile, List<Vector3Int> neighbordCellsIndexesOffset)
    {
        return NeighbordTiles(tile.Cell, neighbordCellsIndexesOffset);
    }

    public List<Tile> ExtremeTiles(List<Tile> tiles, List<Vector3Int> neighbordCellsIndexesOffset, int countNeighbordTiles)
    {
        List<Tile> extremeTiles = new List<Tile>();
        foreach (var tile in tiles)
        {
            if (ExtremeTile(tile, neighbordCellsIndexesOffset, countNeighbordTiles) && !(extremeTiles.Contains(tile)))
                extremeTiles.Add(tile);
        }
        return extremeTiles;
    }

    public List<Tile> ExtremeTiles(List<Cell> cells, List<Vector3Int> neighbordCellsIndexesOffset, int countNeighbordTiles)
    {
        List<Tile> extremeTiles = new List<Tile>();
        foreach (var cell in cells)
        {
            if (ExtremeTile(cell, neighbordCellsIndexesOffset, countNeighbordTiles) && !(extremeTiles.Contains(cell.tile)))
                extremeTiles.Add(cell.tile);
        }
        return extremeTiles;
    }

    public bool ExtremeTile(Tile tile, List<Vector3Int> neighbordCellsIndexesOffset, int countNeighbordTiles)
    {
        var rez = false;
        var neighboringTiles =
            NeighbordTiles(tile, neighbordCellsIndexesOffset);
        if (neighboringTiles.Count < countNeighbordTiles)
            rez = true;
        return rez;
    }

    public bool ExtremeTile(Cell cell, List<Vector3Int> neighbordCellsIndexesOffset, int countNeighbordTiles)
    {
        var rez = false;
        if (cell.tile != null)
            rez = ExtremeTile(cell.tile, neighbordCellsIndexesOffset, countNeighbordTiles);
        return rez;
    }
}
