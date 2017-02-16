using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertMainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.enabled = true;
	}

    public void invertMainCamera() {
        Camera.main.enabled = !Camera.main.enabled;
    }
}
