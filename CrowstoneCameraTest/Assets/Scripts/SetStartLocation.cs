using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartLocation : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (GameManager.gameManager.getPreviousLocation().Equals("Saloon")) {
            gameObject.transform.position = new Vector3(-32.66f, .0099f, -8.2f);
        }
    }
}
