using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHiltFollowRightHand : MonoBehaviour {

    Transform rightHand;
    Transform axe;
    Transform target;


	// Use this for initialization
	void Start () {
        rightHand = GameObject.FindGameObjectWithTag("RightHand").transform;
        axe = transform.parent.transform;
	}
	
	// Update is called once per frame
	void Update () {
        axe.transform.position = rightHand.transform.position - transform.localPosition;
	}
}
