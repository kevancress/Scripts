using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public GameObject playerInventoryPanel;
    public int space = 2;
    public List<InventoryItem> items = new List<InventoryItem>();


    public delegate void onInventoryFull();
    public onInventoryFull onInventoryFullCallback;

    public delegate void onItemChanged();
    public onInventoryFull onItemChangedCallback;

    #region Singleton
    public static InventoryManager instance;

    void Awake()
    {
        if (instance!= null)
        {
            Debug.LogWarning("More than one instance of Inventory Manager Present!");
            return;
        }
        instance = this;
    }

    #endregion

    public bool Add(InventoryItem item)
    {
        if (items.Count >= space)
        {
            InventoryFull();
            return false;
        }
        items.Add(item);
        onItemChangedCallback.Invoke();
        return true;
    }

    private void InventoryFull()
    {
        onInventoryFullCallback.Invoke();
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);
        onItemChangedCallback.Invoke();
    }
}
