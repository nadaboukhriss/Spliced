using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    [TextArea(15, 20)]
    public string description;
}

public enum ItemType
{
    Skull,
    YellowEmerald,
    BlueEmerald
    // add more items here as needed
}
