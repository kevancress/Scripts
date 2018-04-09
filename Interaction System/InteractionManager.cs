using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    public InteractableObject obj;
    public List<InteractableObject> possibleActions = new List<InteractableObject>();

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

    internal void UpdateInteractionManager(InteractableObject newobj)
    {
        obj = newobj;
        if (onUpdateInteractionUICallback != null)
        {
            onUpdateInteractionUICallback.Invoke(); 
        }

        //Debug.Log("UI updated" + lable + action);
       
    }
}
