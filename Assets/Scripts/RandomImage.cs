using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomImage : MonoBehaviour
{
    public List<Sprite> sprites;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
}
