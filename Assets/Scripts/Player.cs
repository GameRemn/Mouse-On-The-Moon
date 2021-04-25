using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMiteorTrigger
{
    public Cell cellPosition;
    public PlayerStatus playerStatus;

    public PlayerStatus PlayerStatus
    {
        get => playerStatus;
        set
        {
            if (value != playerStatus)
            {
                if (value == PlayerStatus.Standing)
                {
                    var downTile =
                        (TileWithWalls) TileGridManager.Instance.cellGrid
                            .cells[
                                TileGridManager.Instance.cellGrid.OddrCoordinatesInIndex(
                                    cellPosition.positionInOddrCoordinates + Vector3Int.down * TileGridManager.Instance.cellGrid.axisDirection)].tile;
                    if (downTile != null && !downTile.IsFull)
                    {
                        StartCoroutine(FallCoroutine(cellPosition, downTile.Cell));
                        return;
                    }
                }
                playerStatus = value;
            }
        }
    }

    public float moveSpeed;
    public AnimationCurve moveCurve;
    public float fallSpeed;
    public AnimationCurve fallCurve;
    public float digTime;

    private void Update()
    {
        if (playerStatus == PlayerStatus.Standing)
        {
            if (Input.GetAxis("Horizontal") == 1)
            {
                CheckAction(Vector2Int.right);
            }
            else if (Input.GetAxis("Horizontal") == -1)
            {
                CheckAction(Vector2Int.left);
            }
            else if (Input.GetAxis("Vertical") == -1)
            {
                CheckAction(Vector2Int.up);
            }
            else if (Input.GetAxis("Vertical") == 1)
            {
                CheckAction(Vector2Int.down);
            }
        }
    }

    public void SetPlayerCell(Cell cell)
    {
        cellPosition = cell;
        transform.position = cellPosition.transform.position;
        ((TileWithWalls) cellPosition.tile).IsFull = false;
    }

    public void CheckAction(Vector2Int direction)
    {
        var nextCellIndex = TileGridManager.Instance.cellGrid.OddrCoordinatesInIndex(new Vector3Int(
            cellPosition.positionInOddrCoordinates.x + direction.x,
            cellPosition.positionInOddrCoordinates.y + direction.y, 0));
        if (nextCellIndex != -1)
        {
            var nextTile = (TileWithWalls)TileGridManager.Instance.cellGrid.cells[nextCellIndex].tile;
            if (nextTile != null)
            {
                if (!nextTile.IsFull)
                {
                    StartCoroutine(MoveCoroutine(cellPosition, nextTile.Cell));
                }
                else
                {
                    StartCoroutine(DigCoroutine(nextTile));
                }
            }
        }
    }

    IEnumerator MoveCoroutine(Cell _last_position, Cell _next_position)
    {
        PlayerStatus = PlayerStatus.Move;
        for(float i = 0; i < 1; i += Time.deltaTime * moveSpeed)
        {
            transform.position = Vector3.LerpUnclamped(_last_position.transform.position, _next_position.transform.position, moveCurve.Evaluate(i));
            yield return null;
        }
        cellPosition = _next_position;
        transform.position = cellPosition.transform.position;
        PlayerStatus = PlayerStatus.Standing;
    }
    
    IEnumerator FallCoroutine(Cell _last_position, Cell _next_position)
    {
        PlayerStatus = PlayerStatus.Fall;
        for(float i = 0; i < 1; i += Time.deltaTime * fallSpeed)
        {
            transform.position = Vector3.LerpUnclamped(_last_position.transform.position, _next_position.transform.position, fallCurve.Evaluate(i));
            yield return null;
        }
        cellPosition = _next_position;
        transform.position = cellPosition.transform.position;
        PlayerStatus = PlayerStatus.Standing;
    }

    IEnumerator DigCoroutine(TileWithWalls tile)
    {
        PlayerStatus = PlayerStatus.Dig;
        yield return new WaitForSeconds(digTime);
        tile.IsFull = false;
        PlayerStatus = PlayerStatus.Standing;
    }

    public void OnMetiorTrigger()
    {
        //конец игры
    }
}

public enum PlayerStatus
{
    Standing,
    Move,
    Dig,
    Fall
}
