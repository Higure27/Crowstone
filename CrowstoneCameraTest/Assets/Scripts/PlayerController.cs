using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float yRotation;
    public bool canMove;
	// Use this for initialization
	void Start () {
        Camera.main.enabled = true;
        canMove = false;
    }

    // Update is called once per frame
    void Update() {
        if (canMove) {
            this.transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            this.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5.0f, 0, 0);
            this.transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * 5f);
        }
    }
}
