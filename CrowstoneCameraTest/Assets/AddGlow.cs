using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlow : MonoBehaviour {

    Transform playerTransform;
    public GameObject name;
    public GameObject glow;
    public float maxDistanceToGlow = 3.5f;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(name != null)
        {
            name.gameObject.SetActive(false);
        }

        if(glow != null)
        {
            glow.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (DistanceBetweenThisAndPlayer() > maxDistanceToGlow) {
            if (name != null) {
                name.gameObject.SetActive(false);
            }
        }
    }

    void OnMouseOver()
    {
        if(DistanceBetweenThisAndPlayer() <= maxDistanceToGlow)
        {
            if (name != null)
            {
                name.gameObject.SetActive(true);
            }

            if (glow != null)
            {
                glow.gameObject.SetActive(true);
            }
        }
    }

    void OnMouseExit()
    {
        if(name != null)
        {
            name.gameObject.SetActive(false);
        }

        if( glow != null)
        {
            glow.gameObject.SetActive(false);
        }
    }

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
