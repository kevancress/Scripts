using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButtonUI : MonoBehaviour {
    internal InteractableObject obj;
    InteractionManager interactionManager;
    public GameObject actionButton;
    public Text key;
    public Text action;


    void Start()
    {
        interactionManager = InteractionManager.instance;
        interactionManager.onUpdateInteractionUICallback += UpdateInteractionButtonUI;
        interactionManager.onNotNearIntObjCallback += DisableInteractionButtonUI;

        action.enabled = false;
        actionButton.SetActive(false);
        
    }

    void LateUpdate()
    {
        if (actionButton.activeSelf)
        {
            if (Input.GetKeyDown("f"))
            {
                CallTriggerAction();

            }
        }
    }

    private void CallTriggerAction()
    {
        interactionManager.TriggerAction(obj);
    }

    private void DisableInteractionButtonUI()
    {
        action.enabled = false;
        actionButton.SetActive(false);
    }

    private void UpdateInteractionButtonUI()
    {
        obj = interactionManager.obj;
        key.text = obj.key;
        action.text = obj.action;
        if (action.text != "" && key.text != "")
        {
            action.enabled = true;
            actionButton.SetActive(true);
        }

        else
        {
            action.enabled = false;
            actionButton.SetActive(false);
        }
        
    }
}
