using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGridManager : MonoSingleton<TileGridManager>
{
    public CellGrid cellGrid;
    public TileGrid tileGrid;
    public ItemGenerator itemGenerator;
    public List<TileWithWalls> hardTiles;
    public List<TileWithWalls> softTiles;
    public Player player;
    private void Start()
    {
        cellGrid.CreateCellGrid();
        tileGrid.CreateTileGrid(cellGrid.cells);
        player.SetPlayerCell(cellGrid.cells[cellGrid.numberOfColumnsRowsLayers.x/2]);
        foreach (var tile in tileGrid.tiles)
        {
            var tileWithWalls = (TileWithWalls) tile;
            if (tileWithWalls.isHard)
            {
                hardTiles.Add(tileWithWalls);
            }
            else
            {
                softTiles.Add(tileWithWalls);
            }
        }
        while (itemGenerator.ItemsCounts.Count > 0)
        {
            var tile = softTiles[Random.Range(0, softTiles.Count)];
            itemGenerator.GetItem(tile.transform.position);
        }

        var downSotTiles = from softTile in softTiles
            where softTile.Cell.positionInOddrCoordinates.y == cellGrid.numberOfColumnsRowsLayers.y - 1
            select softTile;
        List<TileWithWalls> lowerTilesList = new List<TileWithWalls>();
        foreach (var tile in downSotTiles)
        {
            lowerTilesList.Add(tile);
        }
        var specialTile = lowerTilesList[Random.Range(0, lowerTilesList.Count)];
        itemGenerator.GetItem(specialTile.transform.position, true);
    }
}
