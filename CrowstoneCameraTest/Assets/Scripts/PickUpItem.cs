using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItem : MonoBehaviour {

    public GameObject player;
    public GameObject UI;
    public string item;
    public string description;

    private bool inRange;
    private RaycastHit hit;


    private void Start() {
        inRange = false;
        UI.SetActive(false);
    }

    private void Update() {
   
        if (inRange) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)) {
                UI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    GameManager.gameManager.addItem(item, description);
                    UI.SetActive(false);
                    DestroyObject(gameObject);
                }
            }
            else {
                UI.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag.Equals("Player")) {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.tag.Equals("Player")) {
            inRange = false;
            UI.SetActive(false);
        }
    }
}
