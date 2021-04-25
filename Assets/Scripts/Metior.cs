using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metior : MonoBehaviour
{
    public float moveSpeed;
    public AnimationCurve moveCurve;
    public Vector3 lastPosition;
    public List<IMiteorTrigger> triggerObjects;

    private void OnEnable()
    {
        //StartCoroutine(MoveCoroutine(transform.position,lastPosition));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTrigger = other.GetComponent<IMiteorTrigger>();
        if (otherTrigger != null)
        {
            triggerObjects.Add(otherTrigger);
        }
        Debug.Log(triggerObjects.Count);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        var otherTrigger = other.GetComponent<IMiteorTrigger>();
        if (otherTrigger != null)
        {
            triggerObjects.Remove(otherTrigger);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var triggerObject in triggerObjects)
        {
            triggerObject.OnMetiorTrigger();
        }
        Destroy(gameObject);
    }

    IEnumerator MoveCoroutine(Vector3 _last_position, Vector3 _next_position)
    {
        for(float i = 0; i < 1; i += Time.deltaTime * moveSpeed)
        {
            transform.position = Vector3.LerpUnclamped(_last_position, _next_position, moveCurve.Evaluate(i));
            yield return null;
        }
        transform.position = _next_position;
    }
}
