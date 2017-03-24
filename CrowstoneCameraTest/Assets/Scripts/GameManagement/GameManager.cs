//Created by Jared Shaw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// This is a static class that stays throughout the whole game
/// without being destroyed, as such it stores information that is needed
/// accross scenes such as inventory, current day, currency, etc
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager gameManager;
    private string currentLocation = "Town";
    private string previousLocation = "None";
    private float currency;
    private bool isPaused;
    private bool inUI;
    private bool canGlow;
    private Dictionary<string, string> inventory;

    private int currentDay = 1;

    // Use this for initialization
    void Start() {
        inventory = new Dictionary<string, string>();
        currency = 100;
        isPaused = false;
        inUI = false;
        canGlow = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake() {
        //Singleton pattern: if none of these objects exist then keep it from being destroyed
        if (gameManager == null) {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }//if another game manager exists then destroy it
        else if (gameManager != this) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// updates previous location with current location
    /// </summary>
    public void changePreviousLocation() {
        previousLocation = currentLocation;
    }

    /// <summary>
    /// updates current location with the given scene name
    /// </summary>
    /// <param name="sceneName">string</param>
    public void changeCurrentLocation(string sceneName) {
        currentLocation = sceneName;
    }

    /// <summary>
    /// resets the game state, is called when
    /// player is in game and clicks on main menu
    /// </summary>
    public void resetGameState()
    {
        //TODO: finish this function
    }

    /// <summary>
    /// returns the previous location
    /// </summary>
    /// <returns>string</returns>
    public string getPreviousLocation() {
        return previousLocation;
    }

    /// <summary>
    /// returns the current location
    /// </summary>
    /// <returns>string</returns>
    public string getCurrentLocation() {
        return currentLocation;
    }

    /// <summary>
    /// returns the current day
    /// </summary>
    /// <returns>int</returns>
    public int getCurrentDay() {
        return currentDay;
    }

    /// <summary>
    /// sets the current day
    /// </summary>
    /// <param name="day">int</param>
    public void setCurrentDay(int day) {
        currentDay = day;
    }

    /// <summary>
    /// adds the given item and description to inventory if it is not there already
    /// </summary>
    /// <param name="item">string</param>
    /// <param name="description">string</param>
    public void addItem(string item, string description) {
        if (!checkForItem(item))
        {
            inventory.Add(item, description);
            Debug.Log("picked up item: " + item);
        }
    }

    /// <summary>
    /// returns the name of all items in inventory
    /// </summary>
    /// <returns>string[]</returns>
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

    /// <summary>
    /// checks whether an item is in inventory
    /// </summary>
    /// <param name="item">string</param>
    /// <returns>bool</returns>
    public bool checkForItem(string item) {
        if (inventory.ContainsKey(item)) {
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// adds the given amount to total currency
    /// </summary>
    /// <param name="amount">float</param>
    public void modifyCurrency(float amount) {
        currency += amount;
    }

    /// <summary>
    /// returns the total currency
    /// </summary>
    /// <returns>float</returns>
    public float getCurrency() {
        return currency;
    }

    /// <summary>
    /// flips the pause state between paused, unpaused
    /// </summary>
    public void flipPause() {
        isPaused = !isPaused;
    }

    /// <summary>
    /// returns the state of pause
    /// </summary>
    /// <returns>bool</returns>
    public bool getPause() {
        return isPaused;
    }

    /// <summary>
    /// flips the UI state between inUI, not inUI
    /// </summary>
    public void flipInUI() {
        inUI = !inUI;
    }

    /// <summary>
    /// returns the state of UI
    /// </summary>
    /// <returns>bool</returns>
    public bool getInUI() {
        return inUI;
    }

    public bool CanGlow()
    {
        return canGlow;
    }

    public void SetGlow(bool b)
    {
        canGlow = b;
    }
}
