using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject {
	
	Animator doorAnimator;
	bool clicked;
	int doorState;

    public override void Start()
    {
        base.Start();
        doorAnimator = GetComponent<Animator>();
    }

    public override void OnInteraction()
    {
        if (isInCollider)
        {
            base.OnInteraction();
            doorState = CheckDoorState();
            switch (doorState)
            {
                case 1:
                    doorAnimator.SetBool("Open", true);
                    break;
                case 2:
                    doorAnimator.SetBool("Open", false);
                    break;
            }
        }
   
	}

	int CheckDoorState(){
		if (doorAnimator.GetBool ("Open") == false) {
			doorState = 1;
		}
		else if (doorAnimator.GetBool ("Open") == true) {
			doorState = 2;
		}
		return doorState;
	}
}