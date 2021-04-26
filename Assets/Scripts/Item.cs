using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IMiteorTrigger, IPlayerTrigger
{
    public Cell cellPosition;
    public UnityEvent onMetiorTriggerEvent;
    public UnityEvent onPlayerTriggerEvent;

    public void ToMenu(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public bool OnMetiorTrigger()
    {
        onMetiorTriggerEvent.Invoke();
        Destroy(gameObject);
        return false;
    }

    public bool onPlayerTrigger()
    {
        onPlayerTriggerEvent.Invoke();
        Destroy(gameObject);
        return true;
    }
}
