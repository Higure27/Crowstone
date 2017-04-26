using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour {

    public GameObject endingOne;
    public GameObject endingTwo;
    public GameObject endingThree;

	// Use this for initialization
	void Start () {
        setEnding(determineEnding());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int determineEnding()
    {
        return 1;
    }

    public void setEnding(int ending)
    {
        if(ending == 1)
        {
            endingOne.gameObject.SetActive(true);
            endingTwo.gameObject.SetActive(false);
            endingThree.gameObject.SetActive(false);

        }else if(ending == 2)
        {
            endingOne.gameObject.SetActive(false);
            endingTwo.gameObject.SetActive(true);
            endingThree.gameObject.SetActive(false);

        }else if(ending == 3)
        {
            endingOne.gameObject.SetActive(false);
            endingTwo.gameObject.SetActive(false);
            endingThree.gameObject.SetActive(true);
        }
    }
}
