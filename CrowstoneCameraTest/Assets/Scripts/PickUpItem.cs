using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpItem : MonoBehaviour {

    public GameObject player;
    public string item;
    public string description;
    public float x, y, z;

    private NavMeshAgent navAgent;
    private Vector3 playerDest;
    private bool pickedUpObject;

    private void Start() {
        playerDest = new Vector3(x, y, z);
        navAgent = player.GetComponent<NavMeshAgent>();
        pickedUpObject = false;
    }

    private void Update() {
        if (pickedUpObject && navAgent.remainingDistance == 0) {
            DestroyObject(gameObject);
            navAgent.ResetPath();
        }
    }

    private void OnMouseDown() {
        navAgent.SetDestination(playerDest);
        player.GetComponent<Inventory>().addItem(item, description);
        pickedUpObject = true;
    }
}
