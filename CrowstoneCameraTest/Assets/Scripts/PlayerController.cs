using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float yRotation;
	// Use this for initialization
	void Start () {
        Camera.main.enabled = true;
    }

    // Update is called once per frame
    void Update () {
        this.transform.eulerAngles = new Vector3(0f, yRotation, 0f);
        this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 2.0f, 0, 0);
        this.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 2f);
    }
}
