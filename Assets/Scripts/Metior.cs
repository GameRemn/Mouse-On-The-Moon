using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metior : MonoBehaviour
{
    public float moveSpeed;
    public int numberDestroyObjects;
    public int maxDestroyObjects;
    public AnimationCurve moveCurve;
    public Vector3 lastPosition;

    private void OnEnable()
    {
        StartCoroutine(MoveCoroutine(transform.position,new Vector3(transform.position.x, lastPosition.y, transform.position.z)));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var otherTrigger = other.gameObject.GetComponent<IMiteorTrigger>();
        if (otherTrigger != null)
        {
            otherTrigger.OnMetiorTrigger();
            numberDestroyObjects++;
            if(numberDestroyObjects>= maxDestroyObjects)
                Destroy(gameObject);
        }
    }

    IEnumerator MoveCoroutine(Vector3 _last_position, Vector3 _next_position)
    {
        for(float i = 0; i < 1; i += Time.deltaTime * moveSpeed)
        {
            transform.position = Vector3.LerpUnclamped(_last_position, _next_position, moveCurve.Evaluate(i));
            yield return null;
        }
        Destroy(gameObject);
    }
}
