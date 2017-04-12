// Bradley Dawn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDay : MonoBehaviour {
    Transform playerTransform;
    public GameObject dayUnfinishedUI;
    public GameObject dayFinishedUI;
    public float distanceToTrigger = 3.5f;

    private bool inRange;
    private bool changingDay;
    private GameObject player;
    private RaycastHit hit;


    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inRange = false;
        changingDay = false;
        dayUnfinishedUI.SetActive(false);
        dayFinishedUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        /*
        int dayFinished = GameManager.gameManager.getDayStatus();

        if (DistanceBetweenThisAndPlayer() <= distanceToTrigger) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)) {
                if (dayFinished && !changingDay) {
                    dayFinishedUI.SetActive(true);
                }
                else if (!dayFinished && !changingDay) {
                    dayUnfinishedUI.SetActive(true);
                }
                else {
                    dayFinishedUI.SetActive(false);
                    dayUnfinishedUI.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.E) && GameManager.gameManager.getPause() == false) {
                    if (dayFinished) {
                        changeDay();
                    }
                }
            }
            else {
                dayFinishedUI.SetActive(false);
                dayUnfinishedUI.SetActive(false);
            }
        }
        else {
            dayFinishedUI.SetActive(false);
            dayUnfinishedUI.SetActive(false);
        }*/
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
    void changeDay() {
        changingDay = true;
        int day = GameManager.gameManager.getCurrentDay() + 1;
        GameManager.gameManager.setCurrentDay(day);
        GameManager.gameManager.resetLocations();
        LevelManager.Instance.startLoadSpecificScene("Town");
    } 
}
