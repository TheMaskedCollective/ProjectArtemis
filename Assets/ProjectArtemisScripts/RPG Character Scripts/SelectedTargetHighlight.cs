using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTargetHighlight : MonoBehaviour {

    public GameObject highlight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.GetComponentInParent<CharacterHealth>().m_IsCurrentTarget)
        {
            highlight.SetActive(true);
        }
        else
        {
            highlight.SetActive(false);
        }
	}
}
