using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 15.0f, 0, 0);
        this.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 15.0f);
    }
}
