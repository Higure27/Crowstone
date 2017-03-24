// Created by Jared Shaw

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlow : MonoBehaviour {

    Transform playerTransform;

    /// <summary>
    /// this should be a prefab called name that is added as a child to this object
    /// </summary>
    public GameObject nameToDisplay;
    
    /// <summary>
    /// this should be a prefab called glow that is added as a child to this object
    /// </summary>
    public GameObject glow;

    public float distanceToGlow = 3.5f;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(nameToDisplay != null)
        {
            nameToDisplay.gameObject.SetActive(false);
        }

        if(glow != null)
        {
            glow.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {


    }

    /// <summary>
    /// This function is called when the cursor is over the object this
    /// script is attached to
    /// </summary>
    void OnMouseOver()
    {
        if(DistanceBetweenThisAndPlayer() <= distanceToGlow && GameManager.gameManager.CanGlow())
        {
            //activate name
            if (nameToDisplay != null)
            {
                nameToDisplay.gameObject.SetActive(true);
            }

            //activate glow
            if (glow != null)
            {
                glow.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// This function is called when after OnMouseOver() has been called AND
    /// when the cursor is no longer over the object this script is attached to
    /// </summary>
    void OnMouseExit()
    {
        //disable name
        if(nameToDisplay != null)
        {
            nameToDisplay.gameObject.SetActive(false);
        }

        //disable glow
        if( glow != null)
        {
            glow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// returns the distance between the object this script is attached to,
    /// and the player using the distance formula
    /// </summary>
    /// <returns></returns>
    private float DistanceBetweenThisAndPlayer()
    {
        float result = 0.0f;

        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float z1 = transform.position.z;

        float x2 = playerTransform.position.x;
        float y2 = playerTransform.position.y;
        float z2 = playerTransform.position.z;

        //distance formula
        result = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2) + Mathf.Pow((z1 - z2), 2));

        return result;
    }
}
