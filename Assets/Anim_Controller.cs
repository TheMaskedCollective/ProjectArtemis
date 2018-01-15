using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Controller : MonoBehaviour {

	public Animator anim;
    GameObject weapon;
    GameObject wielder;
    GameObject lHandBone;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
        weapon = transform.GetComponentInChildren<BasicAttack>().gameObject;
        wielder = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		//{
  //          SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
  //          foreach(var bone in renderer.bones)
  //          {
  //              string bonename = bone.name;
  //          }
  //          lHandBone = wielder.transform.Find("Bone.026").gameObject;
  //          if(lHandBone != null)
  //          {
  //              weapon.transform.parent = lHandBone.transform;
  //              weapon.transform.localPosition = Vector3.zero;
  //          }
        anim.Play("Attack1");
    }
}
