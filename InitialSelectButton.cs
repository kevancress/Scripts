using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialSelectButton : MonoBehaviour {
    public Button button;
	// Use this for initialization
	void OnAwake () {
        button = gameObject.GetComponent<Button>();
        button.Select();
	}
}
