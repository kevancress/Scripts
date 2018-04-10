using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionLableUI : MonoBehaviour {

    public InteractableObject obj;
    InteractionManager interactionManager;
    GameObject lablePanel;
    public Text lable;
    Collider lableObject;
    public Camera mCamera;
    private RectTransform rt;


    void Start()
    {
        lablePanel = gameObject;
        interactionManager = InteractionManager.instance;
        interactionManager.onUpdateInteractionUICallback += UpdateInteractionLableUI;
        interactionManager.onNotNearIntObjCallback += DisableInteractionLableUI;
        rt = lablePanel.GetComponent<RectTransform>();
        lablePanel.SetActive(false);

    }

    private void UpdateInteractionLableUI()
    {
        int count= interactionManager.possibleActions.Count;
        if (count >= 1)
        {
            obj = interactionManager.possibleActions[count-1];
            if (obj != null)
            {
                lable.text = obj.obj.lable;
                lableObject = obj.GetComponentInParent<Collider>();
                if (lable.text != "")
                {
                    lablePanel.SetActive(true);
                }
 
            }
        }

        else
        {
            lablePanel.SetActive(false);
        }

       


    }

    private void DisableInteractionLableUI()
    {
        lablePanel.SetActive(false);
    }

    void LateUpdate()
    {
        if (lableObject != null && lablePanel.activeSelf)
        {
            Vector3 objPos = lableObject.transform.position;
           

            Vector2 pos = RectTransformUtility.WorldToScreenPoint(mCamera, lableObject.transform.position);
            rt.position = pos;
        }
    }
}
