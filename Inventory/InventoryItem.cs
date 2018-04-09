using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : InteractableObject {
    public ItemData item;
    internal InventoryManager inventory;

    public override void Start()
    {
        obj = item;
        base.Start();
        inventory = InventoryManager.instance;

    }

    public override void OnInteraction()
    {
        if (isInCollider)
        {
            base.OnInteraction();

            PickUp();
            interactionManager.possibleActions.Remove(this);
            interactionManager.UpdateInteractionManager();
        }
    }

    internal void PickUp()
    {
        Debug.Log("pickup" + lable + "by " + player.name);
        item.player = player;
        PlayerInteraction playerscript = player.GetComponent<PlayerInteraction>();
        item.holdPosition = playerscript.holdPosition;
        bool wasPickedUp = inventory.Add(this);
        

        if (wasPickedUp)
        {
            gameObject.SetActive(false);
            interactionManager.obj = null;
            interactionManager.NotNearIntObj();
            isInCollider = false;
        }
      
    }
	
    public virtual void Use()
    {
        //use the Item;
    }

    public virtual void Remove()
    {
        Destroy(this);
    }


}
