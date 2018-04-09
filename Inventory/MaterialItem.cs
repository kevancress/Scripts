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

        }
        else if (isHeld)
        {
            Drop();
        }
    }

    public override void Remove()
    {
        if (item.holdPosition != null)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            gameObject.SetActive(true);
            gameObject.transform.position = item.holdPosition.transform.position;
            gameObject.transform.SetParent(item.holdPosition.transform);
            isHeld = true;
            action = "Drop Item";
            lable = "";
            interactionManager.UpdateInteractionManager(this);
            

        }
        else
        {
            Debug.Log("no holder");
        }
    }

    public void Drop()
    {
        gameObject.transform.SetParent(null);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            gameObject.AddComponent<Rigidbody>();
            rb = gameObject.GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        isHeld = false;
        action = item.action;
    }
}
