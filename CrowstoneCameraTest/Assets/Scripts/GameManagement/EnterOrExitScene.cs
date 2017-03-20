using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnterOrExitScene : MonoBehaviour {

    public string sceneName;
    public GameObject player;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.gameManager.changePreviousLocation();
            GameManager.gameManager.changeCurrentLocation(sceneName);
            //SceneManager.LoadScene(sceneName);
            LevelManager.Instance.SwitchArea(sceneName);
         }
    }
}
