using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItem : InventoryItem{
    public bool isHeld = false;

    public override void OnInteraction()
    {
        if (isInCollider && !isHeld)
        {
            PickUp();
            interactionManager.possibleActions.Remove(this);
            interactionManager.UpdateInteractionManager();

        }
        else if (isHeld)
        {
            interactionManager.Drop();
        }
    }

    public override void Remove()
    {
        if (item.holdPosition != null)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }
            gameObject.SetActive(true);
            gameObject.transform.position = item.holdPosition.transform.position;
            gameObject.transform.SetParent(item.holdPosition.transform);
            isHeld = true;
            item.action = "Drop Item";
            item.lable = "";
            interactionManager.possibleActions.Add(this);
            interactionManager.UpdateInteractionManager();


        }
        else
        {
            Debug.Log("no holder");
        }
    }
}
