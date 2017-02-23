using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlow : MonoBehaviour {

    Transform playerTransform;
    public float maxDistanceToGlow = 3.5f;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseOver()
    {
        if(DistanceBetweenThisAndPlayer() <= maxDistanceToGlow)
        {
            Debug.Log("acceptable distance to glow");
        }
    }

    void OnMouseExit()
    {
        
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
