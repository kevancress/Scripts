
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New BasicAbility", menuName = "Abilities/Basic")]
public class Ability : ScriptableObject {


    public virtual void OnUseBasic()
    {
        Debug.Log("ability basic use");
    }

    public virtual void OnUseHold()
    {
        Debug.Log("ability hold use");
    }

    public virtual void EndHold()
    {
        Debug.Log("end hold");
    }
}
