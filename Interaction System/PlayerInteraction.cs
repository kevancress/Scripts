using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour {
    public bool isNearIntObj;
    public GameObject holdPosition;
    InteractionManager interactionManager;
    InteractableObject currentObjInView;
    InteractableObject lastObjInView;
    


    void Start()
    {
        interactionManager = InteractionManager.instance;
        interactionManager.onNearIntObjCallback += CloseToIntObj;
        interactionManager.onNotNearIntObjCallback += NotCloseToIntObj;
    }

    void Update()
    {
        if (isNearIntObj)
        {
            lastObjInView = currentObjInView;
            currentObjInView = IsLookingAt();
            if (currentObjInView != lastObjInView && currentObjInView !=null )
            {
                if (!interactionManager.possibleActions.Contains(currentObjInView))
                {
                    interactionManager.possibleActions.Add(currentObjInView);
                    interactionManager.UpdateInteractionManager();
                }
                else if (interactionManager.possibleActions.Contains(currentObjInView))
                {
                    int index = interactionManager.possibleActions.IndexOf(currentObjInView);
                    int finalIndex = interactionManager.possibleActions.Count - 1;
                    if (index != finalIndex)
                    {
                        interactionManager.possibleActions.Remove(currentObjInView);
                        interactionManager.possibleActions.Add(currentObjInView);
                        interactionManager.UpdateInteractionManager();
                    }
                  

                    
                }
            }

            
        }
    }

    void CloseToIntObj()
    {
        isNearIntObj = true;
       // Debug.Log("getting close to an interactable object");
    }

    void NotCloseToIntObj()
    {
        isNearIntObj = false;
        //Debug.Log("getting away from an interactable object");
    }

    private InteractableObject IsLookingAt()
    {
        Vector3 playerPos = transform.position;
        Vector3 playerForward = transform.forward;
        Ray ray = new Ray(playerPos, playerForward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.3f))
        {
            InteractableObject obj = hit.collider.GetComponent<InteractableObject>();
                if (obj != null)
                {
                return obj;
                }
        }
        return null;
    }
}
