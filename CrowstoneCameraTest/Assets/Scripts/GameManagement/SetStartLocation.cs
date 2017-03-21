using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartLocation : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (GameManager.gameManager.getPreviousLocation().Equals("Saloon")) {
            gameObject.transform.position = new Vector3(-18.6f, 0f, -20f);
            gameObject.transform.Rotate(0, -140, 0);
        }
        else if (GameManager.gameManager.getPreviousLocation().Equals("Jail")) {
            gameObject.transform.position = new Vector3(-15.71f, 0f, -6.85f);
            gameObject.transform.Rotate(0, 90, 0);
        }
        
    }
}
