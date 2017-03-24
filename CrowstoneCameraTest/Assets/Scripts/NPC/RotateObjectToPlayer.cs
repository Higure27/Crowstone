// Created by Jared Shaw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes the position of the player relative to the object this script is attached to, 
/// and rotates the object towards the player
/// </summary>
public class RotateObjectToPlayer : MonoBehaviour {

    public bool rotateToPlayer;
    public bool mirrorView;
    public float damping = 3.0f; //speed at which the object turns

    private Transform playerTransform;
    private Transform npcTransform;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        npcTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        //if true, rotates this object to the player
        if (rotateToPlayer)
        {
            Vector3 lookPos = playerTransform.position - npcTransform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            if (mirrorView)
            {
                rotation = Quaternion.Inverse(rotation);
            }
            npcTransform.rotation = Quaternion.Slerp(npcTransform.rotation, rotation, Time.deltaTime * damping);
             
        }
	}
}
    