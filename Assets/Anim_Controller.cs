using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Controller : MonoBehaviour {

	public Animator anim;
    GameObject weapon;
    GameObject wielder;
    GameObject lHandBone;
    HumanBodyBones[] test;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
        weapon = transform.GetComponentInChildren<BasicAttack>().gameObject;
        wielder = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("Attack1");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            var test = transform.Find("Bone.018");

            SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.tag == "RightHand")
                {
                    foreach (var bone in renderer.bones)
                    {
                        string bonename = bone.name;
                    }
                    lHandBone = wielder.transform.Find("Bone.026").gameObject;
                    if (lHandBone != null)
                    {
                        weapon.transform.localPosition = lHandBone.transform.position - weapon.GetComponent<AxeHiltFollowRightHand>().transform.localPosition;
                    }
                    break;
                }
            }
        }
    }
}
