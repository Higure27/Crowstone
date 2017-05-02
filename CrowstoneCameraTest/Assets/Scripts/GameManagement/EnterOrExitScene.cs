// Bradley Dawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnterOrExitScene : MonoBehaviour {

    public string sceneName;
    public GameObject player;
    private int day1finished;
    private int day2finished;
    private int day3finished;

    private bool changing = false;
    private void Update() {
    }
    private void OnTriggerEnter(Collider other) {
        grabDayStatuses();
        if (other.tag == "Player") {
            GameManager.gameManager.HUD.SetActive(false);
            if (day1finished == 1) {
                int day = 2;
                GameManager.gameManager.setCurrentDay(day);
                GameManager.gameManager.resetLocations();
                LevelManager.Instance.startLoadSpecificScene("Town");
                DayManager._dayStory.variablesState["day_Complete1"] = 0;
                DayManager._dayStory.variablesState["day"] = day;
            }
            else if (day2finished == 1) {
                int day = 3;
                GameManager.gameManager.setCurrentDay(day);
                GameManager.gameManager.resetLocations();
                LevelManager.Instance.startLoadSpecificScene("Town");
                DayManager._dayStory.variablesState["day_Complete2"] = 0;
                DayManager._dayStory.variablesState["day"] = day;
            }
            else if (day3finished == 1) {
                int day = 4;
                GameManager.gameManager.setCurrentDay(day);
                GameManager.gameManager.resetLocations();
                LevelManager.Instance.startLoadSpecificScene("Town");
                DayManager._dayStory.variablesState["day_Complete3"] = 0;
                DayManager._dayStory.variablesState["day"] = day;
                
            }
            else {
                GameManager.gameManager.changePreviousLocation();
                GameManager.gameManager.changeCurrentLocation(sceneName);
                LevelManager.Instance.SwitchArea(sceneName);
            }
        }
    }

    private void grabDayStatuses() {
        if (DayManager._dayStory != null) {
            day1finished = (int) DayManager._dayStory.variablesState["day_Complete1"];
            day2finished = (int) DayManager._dayStory.variablesState["day_Complete2"];
            day3finished = (int) DayManager._dayStory.variablesState["day_Complete3"];
        }
    }
}
