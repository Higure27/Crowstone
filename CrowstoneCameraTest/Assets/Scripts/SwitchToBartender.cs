using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwitchToBartender : MonoBehaviour {

    public Camera conversationCamera;
    //public Camera mainCamera;
    public GameObject player;
    public float x, y, z;
    private NavMeshAgent navAgent;
    private bool switchCamera;
    private Vector3 playerDest;

    // Use this for initialization
    void Start () {
        playerDest = new Vector3(x, y, z);
        Camera.main.enabled = true;
        switchCamera = false;
        conversationCamera.enabled = false;
        navAgent = player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (switchCamera && navAgent.remainingDistance == 0) {
            conversationCamera.enabled = !conversationCamera.enabled;
            switchCamera = false;
            navAgent.ResetPath();
        }
    }

    private void OnMouseDown() {
        switchCamera = true;
        navAgent.SetDestination(playerDest);
    }
}
