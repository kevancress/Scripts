using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour {
    public Ability attack;
    public Ability passive;
    float timeHeld = 0f;
    float holdThreshold = .25f;
    string fire1 = "Fire1";
    string fire2 = "Fire2";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        #region attack logic
        if (Input.GetButton(fire1))
        {
            timeHeld += 1 * Time.deltaTime;
            if (timeHeld > holdThreshold)
            {
                attack.OnUseHold();
            }
        }
        if (Input.GetButtonUp(fire1) && timeHeld<holdThreshold)
        {
            attack.OnUseBasic();
            timeHeld = 0;
        }
        else if (Input.GetButtonUp(fire1) && timeHeld > holdThreshold)
        {
            attack.EndHold();
            timeHeld = 0;
        }
        #endregion

        #region Passive Logic
        if (Input.GetButton(fire2))
        {
            timeHeld += 1 * Time.deltaTime;
            if (timeHeld > holdThreshold)
            {
                attack.OnUseHold();
            }
        }
        if (Input.GetButtonUp(fire2) && timeHeld < holdThreshold)
        {
            attack.OnUseBasic();
            timeHeld = 0;
        }
        else if (Input.GetButtonUp(fire2) && timeHeld > holdThreshold)
        {
            attack.EndHold();
            timeHeld = 0;
        }
        #endregion



    }
}
