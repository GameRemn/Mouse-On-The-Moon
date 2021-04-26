using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public List<ItemCount> ItemsCounts;
    public Item winItemPrefab;
    public List<Item> createItems;
    public Item GetItem(Vector3 position, bool win = false)
    {
        Item itemPrefab;
        if (win)
        {
            itemPrefab = winItemPrefab;
        }
        else
        {
            itemPrefab = SelectItem();
        }
        var newItem = Instantiate(itemPrefab, transform, false);
        newItem.transform.localPosition = position;
        createItems.Add(newItem);
        return newItem;
    }

    private Item SelectItem()
    {
        var itemCount = ItemsCounts[Random.Range(0, ItemsCounts.Count)];
        itemCount.count--;
        if (itemCount.count <= 0)
            ItemsCounts.Remove(itemCount);
        return itemCount.item;
    }
}

[System.Serializable]
public class ItemCount
{
    public Item item;
    public int count;
}
