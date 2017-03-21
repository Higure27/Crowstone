using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    private string currentLocation = "Town";
    private string previousLocation = "None";
    private float currency;
    private bool isPaused;
    private bool inUI;
    private Dictionary<string, string> inventory;

    private int currentDay = 1;

    // Use this for initialization
    void Start() {
        inventory = new Dictionary<string, string>();
        currency = 100;
        isPaused = false;
        inUI = false;
    }

    // Update is called once per frame
    void Update() {

    }

    void Awake() {
        //Singleton pattern
        if (gameManager == null) {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        else if (gameManager != this) {
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

    public int getCurrentDay() {
        return currentDay;
    }

    public void setCurrentDay(int day) {
        currentDay = day;
    }

    public void addItem(string item, string description) {
        if (!checkForItem(item))
        {
            inventory.Add(item, description);
            Debug.Log("picked up item: " + item);
        }
    }

    public string[] getAllItems()
    {
        string[] items;

        if (inventory != null)
        {
           items = new string[inventory.Count];
            items = inventory.Keys.ToArray<string>(); 
        }
        else
        {
            items = new string[1];
            items[0] = "empty";
        }
        return items;
    }

    public bool checkForItem(string item) {
        if (inventory.ContainsKey(item)) {
            return true;
        }
        else {
            return false;
        }
    }

    public void modifyCurrency(float amount) {
        currency += amount;
    }

    public float getCurrency() {
        return currency;
    }

    public void flipPause() {
        isPaused = !isPaused;
    }

    public bool getPause() {
        return isPaused;
    }

    public void flipInUI() {
        inUI = !inUI;
    }

    public bool getInUI() {
        return inUI;
    }
}
