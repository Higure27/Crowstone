﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SwitchToGambler : MonoBehaviour {

    public Camera conversationCamera;
    //public Camera mainCamera;
    public GameObject player;
    public float x, y, z;
    private NavMeshAgent navAgent;
    private bool switchCamera;
    private Vector3 playerDest;
    public Transform dialogue;

    // Use this for initialization
    void Start() {
        playerDest = new Vector3(x, y, z);
        Camera.main.enabled = true;
        switchCamera = false;
        conversationCamera.enabled = false;
        navAgent = player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (switchCamera && navAgent.remainingDistance == 0) {
            SwitchCamera();
            switchCamera = false;
            navAgent.ResetPath();

            //Dialogue Code
            var d = Instantiate(dialogue);
            ConversationUI dialogueUI = d.gameObject.GetComponentInChildren<ConversationUI>();
            dialogueUI.fileInput = "Gambler.txt";
            dialogueUI.startingLabel = "a";
            d.transform.parent = this.transform;
        }
    }

    private void OnMouseDown() {
        if (!GameObject.Find("GameGUI").GetComponent<UserInterface>().OnPauseScreen())
        {
            switchCamera = true;
            navAgent.SetDestination(playerDest);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
    }

    public void SwitchCamera()
    {
        conversationCamera.enabled = !conversationCamera.enabled;
    }
}
