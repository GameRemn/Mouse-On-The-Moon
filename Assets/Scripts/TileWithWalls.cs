using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TileWithWalls : Tile, IMiteorTrigger
{
    public GameObject particlesPrefab;
    private bool isFull = true;
    public bool IsFull
    {
        get => isFull;
        set
        {
            if(value != isFull && value == false)
            {
                isFull = false;
                GetComponent<SpriteRenderer>().sprite = backSprite;
                CheckWalls();
            }
        }
    }

    [FormerlySerializedAs("isSolid")] public bool isHard;
    public Sprite backSprite;
    public SpriteRenderer topWall, rightWall, downWall, leftWall;
    public List<TileWithWalls> neighbordTilesWithWalls;

    public void CheckWalls()
    {
        if (!isFull)
        {
            if (neighbordTilesWithWalls.Count == 0)
            {
                var neighbordTiles =
                    TileGridManager.Instance.tileGrid.NeighbordTiles(cell, NeighbordCellsIndexesOffset.XYCross);
                foreach (var neighbordTile in neighbordTiles)
                {
                    if(neighbordTile == null)
                        continue;
                    var neighbordTileWithWalls = (TileWithWalls) neighbordTile;
                    if (neighbordTileWithWalls != null)
                        neighbordTilesWithWalls.Add(neighbordTileWithWalls);
                }
            }
            foreach (var neighbordTileWithWalls in neighbordTilesWithWalls)
            {
                if (!neighbordTileWithWalls.IsFull)
                {
                    var direktion = neighbordTileWithWalls.cell.positionInOddrCoordinates -
                                    cell.positionInOddrCoordinates;
                    if (direktion == Vector3Int.down)
                    {
                        topWall.enabled = false;
                        neighbordTileWithWalls.downWall.enabled = false;
                    }
                    else if (direktion == Vector3Int.left)
                    {
                        leftWall.enabled = false;
                        neighbordTileWithWalls.rightWall.enabled = false;
                    }
                    else if (direktion == Vector3Int.up)
                    {
                        downWall.enabled = false;
                        neighbordTileWithWalls.topWall.enabled = false;
                    }
                    else if (direktion == Vector3Int.right)
                    {
                        rightWall.enabled = false;
                        neighbordTileWithWalls.leftWall.enabled = false;
                    }
                }
            }
        }
    }

    public bool OnMetiorTrigger()
    {
        StartCoroutine(ParticalSystemLife());
        if (IsFull)
        {
            IsFull = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
            return true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            enabled = false;
            return false;
        }
    }

    public IEnumerator ParticalSystemLife()
    {
        var go = Instantiate(particlesPrefab, transform.position, transform.rotation);
        var p = go.GetComponent<ParticleSystem>();
        p.Play();
        yield return new WaitForSeconds(1f);
        Destroy(go);
    }
}
