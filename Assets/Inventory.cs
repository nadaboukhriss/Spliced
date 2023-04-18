using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{


    public List<ItemObject> items;

    public Inventory()
    {
        items = new List<ItemObject>();
    }
    public void AddItem(ItemObject _item)
    {
        if (!HasItem(_item.type))
        {
            items.Add(_item);
        }
    }

    public bool HasItem(ItemType type)
    {
        foreach (ItemObject item in items)
        {
            if (item.type == type)
                return true;
        }
        return false;
    }

    public List<ItemObject> GetItems()
    {
        return items;
    }


}
