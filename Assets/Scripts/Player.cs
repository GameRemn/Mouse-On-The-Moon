using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Cell cellPosition;
    public PlayerStatus playerStatus;
    public float moveSpeed;
    public float digSpeed;
    public AnimationCurve moveCurve;

    private void Update()
    {
        if (playerStatus == PlayerStatus.Standing)
        {
            if (Input.GetAxis("Horizontal") == 1)
            {
                Move(Vector2Int.right);
            }
            else if (Input.GetAxis("Horizontal") == -1)
            {
                Move(Vector2Int.left);
            }
            else if (Input.GetAxis("Vertical") == 1)
            {
                Move(Vector2Int.up);
            }
            else if (Input.GetAxis("Vertical") == -1)
            {
                Move(Vector2Int.down);
            }
        }
    }

    public void Move(Vector2Int moveDirection)
    {
        var nextCellIndex = TileGridManager.Instance.cellGrid.OddrCoordinatesInIndex(new Vector3Int(
            cellPosition.positionInOddrCoordinates.x + moveDirection.x,
            cellPosition.positionInOddrCoordinates.y + moveDirection.y, 0));
        if (nextCellIndex != -1)
        {
            var nextCell = TileGridManager.Instance.cellGrid.cells[nextCellIndex];
            StartCoroutine(MoveCoroutine(cellPosition, nextCell));
        }
    }
    
    IEnumerator MoveCoroutine(Cell _last_position, Cell _next_position)
    {
        playerStatus = PlayerStatus.Move;
        for(float i = 0; i < 1; i += Time.deltaTime * moveSpeed)
        {
            transform.position = Vector3.LerpUnclamped(_last_position.transform.position, _next_position.transform.position, moveCurve.Evaluate(i));
            yield return null;
        }
        cellPosition = _next_position;
        playerStatus = PlayerStatus.Standing;
    }

    IEnumerator DigCoroutine()
    {
        playerStatus = PlayerStatus.Dig;
        yield return new WaitForSeconds(digSpeed);
        playerStatus = PlayerStatus.Standing;
    }
}

public enum PlayerStatus
{
    Standing,
    Move,
    Dig
}
