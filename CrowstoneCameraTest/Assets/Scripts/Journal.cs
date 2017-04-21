using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour {

    public Text currentTask;
    public GameObject HUD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(HUD.activeSelf);
            if (HUD.activeSelf)
                HUD.SetActive(false);
            else
                HUD.SetActive(true);
            
            if (HUD.activeSelf) {
                if (DayManager._dayStory != null) {
                    currentTask.text = (string)DayManager._dayStory.variablesState["currTask"];
                }
            }
        }
	}
}
