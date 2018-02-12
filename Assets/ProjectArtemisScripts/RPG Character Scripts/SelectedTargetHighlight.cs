using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTargetHighlight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.GetComponentInParent<CharacterHealth>().m_IsCurrentTarget)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
