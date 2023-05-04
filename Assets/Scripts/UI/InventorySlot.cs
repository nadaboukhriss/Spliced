using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{

    [SerializeField]
    private Image itemIcon;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    public void Setup(ItemObject item)
    {
        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        itemDescription.text = item.description;
    }
}
