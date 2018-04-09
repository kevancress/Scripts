using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    public InteractableObject obj;
    public List<InteractableObject> possibleActions = new List<InteractableObject>();
    int count;

    public delegate void NearInteractableObject();
    public NearInteractableObject onNearIntObjCallback;

    public delegate void NotNearInteractableObject();
    public NearInteractableObject onNotNearIntObjCallback;

    public delegate void OnButtonTrigger();
    public OnButtonTrigger onButtonTriggerCallback;

    public delegate void UpdateInteractionUI();
    public UpdateInteractionUI onUpdateInteractionUICallback;
    #region Singleton
    public static InteractionManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Interaction Manager Found!!");
            return;
        }
        instance = this;
    }
    #endregion

    public void NearIntObj()
    {
        if (onNearIntObjCallback != null)
        {
            onNearIntObjCallback.Invoke();
        }
    }

    public void TriggerAction(InteractableObject obj)
    {
        obj.OnInteraction();
    }

    public void NotNearIntObj()
    {
        if (onNotNearIntObjCallback != null)
        {
            onNotNearIntObjCallback.Invoke();
        }
    }

    internal void UpdateInteractionManager()
    {
        if (onUpdateInteractionUICallback != null)
        {
            onUpdateInteractionUICallback.Invoke(); 
        }

        //Debug.Log("UI updated" + lable + action);
       
    }

    internal void RemoveFromPossibleActions(InteractableObject obj)
    {
        possibleActions.Remove(obj);
        UpdateInteractionManager();
    }


    internal void Drop()
    {

        int count = possibleActions.Count;
        int i = 0;
        while (i < count)
        {
            if (possibleActions[i].GetComponent<MaterialItem>() != null && possibleActions[i].GetComponent<MaterialItem>().isHeld)
            {
                Rigidbody rb = possibleActions[i].gameObject.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    possibleActions[i].gameObject.AddComponent<Rigidbody>();
                    rb = possibleActions[i].gameObject.GetComponent<Rigidbody>();
                }
                rb.isKinematic = false;

                if (i < count)
                {
                    possibleActions[i].gameObject.GetComponent<MaterialItem>().isHeld = false;
                    possibleActions[i].gameObject.GetComponent<MaterialItem>().action = possibleActions[i].obj.action;
                    possibleActions[i].gameObject.GetComponent<MaterialItem>().lable = possibleActions[i].obj.lable;
                    possibleActions[i].gameObject.transform.SetParent(null);
                    possibleActions.Remove(possibleActions[i]);
                    count = possibleActions.Count;
                }
                UpdateInteractionManager();
            }
            else
            {
                i++;
            }
        }
    }
}
