using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour {

    public GameObject endingOne_pic1;
    public GameObject endingOne_pic2;
    public GameObject endingTwo_pic1;
    public GameObject endingTwo_pic2;
    public GameObject endingThree_pic1;
    public GameObject endingThree_pic2;

    private GameObject picOne;
    private GameObject picTwo;


    private bool onFirst;
    private bool onSecond;

	// Use this for initialization
	void Start () {
        onFirst = true;
        onSecond = false;
        setEnding(determineEnding());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && onFirst) {
            picOne.SetActive(false);
            picTwo.SetActive(true);
            onSecond = true;
            onFirst = false;
        }
        else if (Input.anyKeyDown && onSecond) {
            LevelManager.Instance.startLoadSpecificScene("Start Menu");
            GameManager.gameManager.resetGameState();
        }
    }

    public int determineEnding()
    {
        if (DayManager._dayStory != null)
            return (int)DayManager._dayStory.variablesState["Ending"];
        else return 0;
    }

    public void setEnding(int ending)
    {
        if(ending == 1) 
        {
            picOne = endingOne_pic1;
            picTwo = endingOne_pic2;
            endingOne_pic1.SetActive(true);
        }
        else if(ending == 2)
        {
            picOne = endingTwo_pic1;
            picTwo = endingTwo_pic2;
            endingTwo_pic1.SetActive(true);
        }
        else if(ending == 3)
        {
            picOne = endingThree_pic1;
            picTwo = endingThree_pic2;
            endingThree_pic1.SetActive(true);
        }
    }
}
