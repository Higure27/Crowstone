// Bradley Dawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartLocation : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (GameManager.gameManager.getPreviousLocation().Equals("Saloon")) {
            gameObject.transform.position = new Vector3(-18.6f, 0f, -20f);
            gameObject.transform.Rotate(0, -50, 0);
        }
        else if (GameManager.gameManager.getPreviousLocation().Equals("Jail")) {
            gameObject.transform.position = new Vector3(-15.71f, 0f, -6.85f);
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if (GameManager.gameManager.getPreviousLocation().Equals("BankInterior")) {
            gameObject.transform.position = new Vector3(-6.553f, 0f, -8.411f);
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if (GameManager.gameManager.getPreviousLocation().Equals("SchoolInterior")) {
            gameObject.transform.position = new Vector3(.09f, 0f, -8.23f);
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if (GameManager.gameManager.getPreviousLocation().Equals("Town")) {
            if (GameManager.gameManager.getCurrentLocation().Equals("Saloon")) {
                gameObject.transform.position = new Vector3(63.61f, -7.67f, 68.81f);
                gameObject.transform.Rotate(0, -25f, 0);
            }
        }

    }
}
