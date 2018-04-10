using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightManager : MonoBehaviour {

    #region Singleton
    public static DaylightManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Daylight Manager Present!");
            return;
        }
        instance = this;
    }

    #endregion

    public Light sun;
	public float secondsPerDay = 1440;
    public float dimLight = .1f;

	[Range (0,1)] public float currentTimeOfDay = 0f;
	float timeMultiplier = 1f;
	float sunInitialIntensity;
    internal bool isDimming = false;

	void Start() {
		sunInitialIntensity = sun.intensity;
	}

	void Update() {

		UpdateSun ();

		currentTimeOfDay += (Time.deltaTime / secondsPerDay) * timeMultiplier;
		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
		}

        if (isDimming)
        {
            float currentIntensity = sun.intensity;
            sun.intensity = Mathf.Lerp(currentIntensity, dimLight, .2f * Time.deltaTime);
        }
	}

	void UpdateSun(){


		sun.transform.rotation = Quaternion.Euler ((currentTimeOfDay * 360f) +45, 170 ,0);


	}

    internal void DimSun()
    {
        isDimming = true;
        
    }

    internal void ResetIntensity()
    {
        sun.intensity = 2f;
        isDimming = false;
    }
}
