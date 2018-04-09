using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public Transform playerInventoryPanel;
    public GameObject inventoryContainer;
    public GameObject optionsPanel;
    InventorySlot selectedslot;

    InventorySlot[] slots;

    InventoryManager inventory;

    TimeManager timeManager;


    #region Singleton
    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory UI Present!");
            return;
        }
        instance = this;
    }

    #endregion
    // Use this for initialization
    void Start () {
        timeManager = TimeManager.instance;
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = playerInventoryPanel.GetComponentsInChildren<InventorySlot>();
        optionsPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            OpenInventoryUI();
        }
		
	}

    private void OpenInventoryUI()
    {
        if (!inventoryContainer.activeSelf)
        {
            inventoryContainer.SetActive(true);
            timeManager.MenuTime();
        }
        else if (inventoryContainer.activeSelf)
        {
            inventoryContainer.SetActive(false);
            timeManager.NormalTime();
        }
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void OnSlotClick(InventorySlot slot)
    {
        
        if (slot.item != null)
        {
            Debug.Log("inventory button clicked contains" + slot.item.name);
            selectedslot = slot;
            ToggelOptionsPanel();
        }
    }

    public void UseItem()
    {
        Debug.Log("Item Used");
        // implement shared inventory
        inventory.Remove(selectedslot.inventoryItem);
        ToggelOptionsPanel();

    }

    public void DropItem()
    {
        // implement shared inventory
        selectedslot.inventoryItem.Remove();
        inventory.Remove(selectedslot.inventoryItem);
        ToggelOptionsPanel();
    }

    public void ShareItem()
    {
        Debug.Log("Item Shared");
        // implement shared inventory
        inventory.Remove(selectedslot.inventoryItem);
        ToggelOptionsPanel();
    }


    public void ToggelOptionsPanel()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Button button = slots[i].GetComponentInChildren<Button>();
            button.interactable = !button.IsInteractable() ;
        }

        optionsPanel.SetActive(!optionsPanel.activeSelf);
        Debug.Log("togeling options panel");
    }
}
