using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    public delegate void pausePressed();
    public static event pausePressed onPausePressed;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Pause))
        {
            Debug.Log("Pause was pressed");
            if(onPausePressed != null)
            {
                onPausePressed();
            }
        }
	}
}
