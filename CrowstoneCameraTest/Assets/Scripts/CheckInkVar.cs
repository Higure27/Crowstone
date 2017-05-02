using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInkVar : MonoBehaviour {

    public GameObject obj;
    public string inkVar;
    public int dayNum;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkForVar();
	}

    void checkForVar() {
        if (DayManager._dayStory != null) {
            if (GameManager.gameManager.getCurrentDay() == dayNum) {
                if ((int) DayManager._dayStory.variablesState[inkVar] == 1) {
                    obj.SetActive(true);
                }
                else {
                    obj.SetActive(false);
                }
            }
        }
    }
}
