using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeController : MonoBehaviour {

	public Light sun;
	public float secondsPerDay = 1440;

	[Range (0,1)] public float currentTimeOfDay = 0f;
	float timeMultiplier = 1f;
	float sunInitialIntensity;

	void Start() {
		sunInitialIntensity = sun.intensity;
	}

	void Update() {

		UpdateSun ();

		currentTimeOfDay += (Time.deltaTime / secondsPerDay) * timeMultiplier;

		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
		}
	}

	void UpdateSun(){


		sun.transform.rotation = Quaternion.Euler ((currentTimeOfDay * 360f) +45, 170 ,0);

		float intensityMultiplier = 1;
		sun.intensity= sunInitialIntensity*intensityMultiplier;


	}
}
