﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    private string currentLocation = "Jail";
    private string previousLocation = "None";

    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update () {

    }

    void Awake() {
        //Singleton pattern
        if (gameManager == null) {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        else if(gameManager != this) {
            Destroy(gameObject);
        }
    }

    public void changePreviousLocation() {
        previousLocation = currentLocation;
    }

    public void changeCurrentLocation(string sceneName) {
        currentLocation = sceneName;
    }

    public string getPreviousLocation() {
        return previousLocation;
    }

    public string getCurrentLocation() {
        return currentLocation;
    }
}
