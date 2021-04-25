using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridManager : MonoSingleton<TileGridManager>
{
    public CellGrid cellGrid;
    public TileGrid tileGrid;
    public Player player;
    private void Start()
    {
        cellGrid.CreateCellGrid();
        tileGrid.CreateTileGrid(cellGrid.cells);
        player.SetPlayerCell(cellGrid.cells[cellGrid.numberOfColumnsRowsLayers.x/2]);
    }
}
