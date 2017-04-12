// Bradley Dawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDailyTasks : MonoBehaviour {

    Transform playerTransform;
    public GameObject taskPanel;
    public GameObject UI;
    public Text taskBoard;
    public float distanceToTrigger = 3.5f;

    private bool inRange;
    private bool taskPanelActive;
    private GameObject player;
    private RaycastHit hit;


    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        inRange = false;
        taskPanelActive = false;
        UI.SetActive(false);
        taskPanel.SetActive(false);
        //taskBoard.text = GameManager.gameManager.getTask();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {

        if (DistanceBetweenThisAndPlayer() <= distanceToTrigger) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)) {
                if (!taskPanelActive) {
                    UI.SetActive(true);
                }
                else {
                    UI.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.E) && GameManager.gameManager.getPause() == false) {
                    if (taskPanelActive) {
                        taskPanel.SetActive(false);
                        taskPanelActive = false;
                        player.GetComponentInChildren<FirstPersonController>().enabled = true;
                        GameManager.gameManager.flipInUI();
                    }
                    else {
                        taskPanel.SetActive(true);
                        taskPanelActive = true;
                        player.GetComponentInChildren<FirstPersonController>().enabled = false;
                        GameManager.gameManager.flipInUI();
                    }
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
