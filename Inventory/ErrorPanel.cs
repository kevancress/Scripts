using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour {
    public GameObject errorBackground;
    InventoryManager inventory;
    public Text errorText;
    public float delay = 1f;

	// Use this for initialization
	void Start () {

        inventory = InventoryManager.instance;
        inventory.onInventoryFullCallback += InventoryFullError;

        errorBackground.SetActive(false);

		
	}

    private void InventoryFullError()
    {
        errorText.text = "Your Inventory is Full";
        errorBackground.SetActive(true);
        Invoke("CloseErrorPanel", delay);
        
    }

    void CloseErrorPanel()
    {
        errorBackground.SetActive(false);
    }
}
