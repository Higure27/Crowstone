using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour {


    public GameObject panel;

	// Use this for initialization
	void Start () {
        panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnMouseEnter() {
        panel.SetActive(true);
    }

    private void OnMouseExit() {
        panel.SetActive(false);
    }
}
