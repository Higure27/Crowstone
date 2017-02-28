using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    private string currentLocation = "Jail";
    private string previousLocation = "None";
    private Dictionary<string, string> inventory;

    private int currentDay = 1;

    // Use this for initialization
    void Start () {
        inventory = new Dictionary<string, string>();
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

    public int getCurrentDay()
    {
        return currentDay;
    }

    public void setCurrentDay(int day)
    {
        currentDay = day;
    }

    public void addItem(string item, string description) {
        inventory.Add(item, description);
    }

    public bool checkForItem(string item) {
        if (inventory.ContainsKey(item)) {
            return true;
        }
        else {
            return false;
        }
    }
}
