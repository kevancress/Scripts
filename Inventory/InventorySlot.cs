using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public ItemData item;
    public InventoryItem inventoryItem;
    public Image icon;
    InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = InventoryUI.instance;
    }

    public void AddItem(InventoryItem newItem)
    {
        inventoryItem = newItem;
        item = inventoryItem.item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        inventoryItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClick(InventorySlot slot)
    {
        inventoryUI.OnSlotClick(slot);
    }
}
