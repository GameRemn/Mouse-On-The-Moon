using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IMiteorTrigger, IPlayerTrigger
{
    public Cell cellPosition;
    public UnityEvent onMetiorTriggerEvent;
    public UnityEvent onPlayerTriggerEvent;

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
