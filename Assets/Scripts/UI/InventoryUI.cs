using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject itemSlotPrefab;

    [SerializeField]
    private RectTransform contentPanel;


    Dictionary<ItemType, GameObject> itemsDisplayed = new Dictionary<ItemType, GameObject>();

    public void Start()
    {
        itemsDisplayed.Clear();
        CreateDisplay();
    }

    /*private void Update()
    {
        UpdateDisplay();
    }*/

    public void CreateDisplay()
    {
        foreach (ItemObject item in player.inventory.GetItems())
        {
            AddInventorySlot(item);
        }
    }

    private void Update()
    {
        UpdateDisplay();
    }
    private void AddInventorySlot(ItemObject item)
    {
        var obj = Instantiate(itemSlotPrefab);
        obj.GetComponent<InventorySlot>().Setup(item);
        obj.transform.SetParent(contentPanel, false);
        itemsDisplayed.Add(item.type, obj);
    }
    public void UpdateDisplay()
    {
        foreach (ItemObject item in player.inventory.GetItems())
        {
            if (!itemsDisplayed.ContainsKey(item.type))
            {
                AddInventorySlot(item);
            }
        }
    }
    public void ToggleInventory()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            UpdateDisplay();
        }
    }


}
