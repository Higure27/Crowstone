using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        Debug.Log("hovering over: " + gameObject.name);
    }

    void OnMouseExit()
    {
        Debug.Log("no longer hovering over: " + gameObject.name);
    }
}
