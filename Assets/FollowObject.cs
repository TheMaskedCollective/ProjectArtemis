using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChild : MonoBehaviour {

    public GameObject followTarget;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    

	// Use this for initialization
	void Start ()
    {
        transform.position = followTarget.transform.position + (new Vector3 (xOffset, yOffset, zOffset));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = followTarget.transform.position + (new Vector3(xOffset, yOffset, zOffset));
    }
}
