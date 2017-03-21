using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItem : MonoBehaviour {

    Transform playerTransform;
    public GameObject UI;
    public string item;
    public string description;
    public float distanceToTrigger = 3.5f;

    private bool inRange;
    private RaycastHit hit;


    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inRange = false;
        UI.SetActive(false);
    }

    private void Update() {
   
        if (DistanceBetweenThisAndPlayer() <= distanceToTrigger && GameManager.gameManager.getPause() == false) {
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
        else {
            UI.SetActive(false);
        }
    }

    private float DistanceBetweenThisAndPlayer() {
        float result = 0.0f;

        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float z1 = transform.position.z;

        float x2 = playerTransform.position.x;
        float y2 = playerTransform.position.y;
        float z2 = playerTransform.position.z;

        //distance formula
        result = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2) + Mathf.Pow((z1 - z2), 2));

        return result;
    }
}
