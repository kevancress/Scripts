using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public GameObject player;
    public GameObject oponent;



    #region Singleton
    public static CombatManager instance;

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


}
