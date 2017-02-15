using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour {

    NavMeshAgent navAgent;

    public AnimationClip run;
    public AnimationClip idle;
    public Camera mainCamera;
    public int rotationSpeed = 500;

    private Quaternion permRotation;

    private bool isRotating;
    private Vector3 direction;
    private Quaternion lookRotation;
    private RaycastHit hit;


    IEnumerator RotateAgent(Quaternion currentRotation, Quaternion targetRotation) {

        isRotating = true; 
        while(currentRotation != targetRotation) {
            transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            currentRotation = transform.rotation;
            yield return 1;
        }
        isRotating = false;
        navAgent.SetDestination(hit.point);
    }


    // Use this for initialization
    void Start () {
        permRotation = gameObject.transform.rotation;
        navAgent = GetComponent<NavMeshAgent>();
        isRotating = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (navAgent.remainingDistance == 0) {
            navAgent.ResetPath();
        }


        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        gameObject.transform.rotation = permRotation;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                if (navAgent.remainingDistance > 0)
                    navAgent.destination = gameObject.transform.localPosition;
                direction = (hit.point - transform.position).normalized;
                lookRotation = Quaternion.LookRotation(direction);

                //StartCoroutine(RotateAgent(transform.rotation, lookRotation));
                navAgent.SetDestination(hit.point);
            }
        }
	}
}
