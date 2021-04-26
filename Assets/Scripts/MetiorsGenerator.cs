using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class MetiorsGenerator : MonoBehaviour
{
    public List<Metior> metiorPrefabs;
    public float minXPosition, maxXPosition;
    public float delayTime;

    private void Start()
    {
        StartCoroutine(GenCoroutine());
    }

    public IEnumerator GenCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(metiorPrefabs[Random.Range(0, metiorPrefabs.Count)], new Vector3(Random.Range(minXPosition, maxXPosition), transform.position.y, transform.position.z), Quaternion.identity, transform);
        StartCoroutine(GenCoroutine());
    }
}
