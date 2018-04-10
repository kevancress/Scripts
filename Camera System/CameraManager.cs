using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Camera mainCamera;

    #region Singleton
    public static CameraManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one Camera Manager Found!!");
            return;
        }
        instance = this;
    }
    #endregion
}
