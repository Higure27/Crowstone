using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateNPCtoCamera : MonoBehaviour {

    public bool rotateToPlayer;
    public float damping = 3.0f;

    private Transform playerTransform;
    private Transform npcTransform;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        npcTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateToPlayer)
        {
            Vector3 lookPos = playerTransform.position - npcTransform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            npcTransform.rotation = Quaternion.Slerp(npcTransform.rotation, rotation, Time.deltaTime * damping);
        }
	}
}
    