// Bradley Dawn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnterOrExitScene : MonoBehaviour {

    public string sceneName;
    public GameObject player;
    private int dayStatus;
    private bool changing = false;
    private void Update() {
        if (DayManager._dayStory != null)
            dayStatus = GameManager.gameManager.getDayStatus();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (dayStatus == 1 && !changing) {
                Debug.Log("CHANGE");
                changing = true;
                int day = GameManager.gameManager.getCurrentDay() + 1;
                GameManager.gameManager.setCurrentDay(day);
                GameManager.gameManager.resetLocations();
                LevelManager.Instance.startLoadSpecificScene("Town");
                changing = false;
            }
            else {
                GameManager.gameManager.changePreviousLocation();
                GameManager.gameManager.changeCurrentLocation(sceneName);
                LevelManager.Instance.SwitchArea(sceneName);
            }
        }
    }
}
