using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public ObjectData obj;
    internal InteractionManager interactionManager;
    //internal string action;
    //internal string lable;
    //internal string key;
    public GameObject InteractionArea;
    internal GameObject player;
    internal bool hasInteracted;
    SphereCollider interactionCollider;
    internal bool isInCollider;

    public virtual void Start()
    {
        //action = obj.action;
        //lable = obj.lable;
        //key = obj.key;

        interactionManager = InteractionManager.instance;

        if (InteractionArea == null)
        {
            InteractionArea = gameObject;
        }
        interactionCollider = InteractionArea.GetComponent<SphereCollider>();
        if (interactionCollider == null)
        {
            InteractionArea.AddComponent<SphereCollider>();
            interactionCollider = InteractionArea.GetComponent<SphereCollider>();
            interactionCollider.isTrigger = true;
            interactionCollider.radius = interactionCollider.radius * 2;
            
        }
    }

    public virtual void OnInteraction()
    {
        //do something;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionManager.NearIntObj();
            Debug.Log(obj.action + obj.lable);
            isInCollider = true;
            player = other.gameObject;
        }
    }
   
    public void OnTriggerExit()
    {
        interactionManager.NotNearIntObj();
        Debug.Log(obj.action + obj.lable);
        isInCollider = false;
        interactionManager.RemoveFromPossibleActions(gameObject.GetComponent<InteractableObject>());
    }
    /*
   
   */
}
