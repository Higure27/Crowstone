using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnterOrExitScene : MonoBehaviour {

    public string SceneName;
    public GameObject player;
    public float x, y, z;
    private NavMeshAgent navAgent;
    private Vector3 playerDest;
    private bool headingToDoor;

    private void Start() {
        playerDest = new Vector3(x, y, z);
        navAgent = player.GetComponent<NavMeshAgent>();
        headingToDoor = false;
    }

    private void Update() {
        if (headingToDoor && navAgent.remainingDistance == 0) {
            headingToDoor = false;
            navAgent.ResetPath();
            SceneManager.LoadScene(SceneName);
        }
    }

    private void OnMouseDown() {
        navAgent.SetDestination(playerDest);
        headingToDoor = true;
    }
}
