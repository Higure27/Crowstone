using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToConversationCamera : MonoBehaviour {

    public Camera conversationCamera;
    public Camera mainCamera;
    private bool switchCamera;


	// Use this for initialization
	void Start () {
        switchCamera = false;
        mainCamera.enabled = true;
        conversationCamera.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (switchCamera) {
            mainCamera.enabled = !mainCamera.enabled;
            conversationCamera.enabled = !conversationCamera.enabled;
            switchCamera = false;
        }
	}

    private void OnMouseDown() {
        switchCamera = true;
    }
}
