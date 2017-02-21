using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private Dictionary<string, string> inventory;

    private void Awake() {
       // DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
		inventory = new Dictionary<string, string>();
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
