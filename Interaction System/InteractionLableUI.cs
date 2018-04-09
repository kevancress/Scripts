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
        obj = interactionManager.obj;
        lable.text = obj.lable;
        lableObject = interactionManager.obj.GetComponentInParent<Collider>();
        if (lable.text != "")
        {
            lablePanel.SetActive(true);
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
