using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform targetObject;
    Transform privateTargetObject;

    float targetObjectIndex = 0;
    float fov = 90;

    GameObject nearestTarget;
    GameObject nextTarget;
    GameObject previousTarget;
    List<GameObject> allTargets;
    List<GameObject> allVisibleTargets;
    List<GameObject> m_PreviousTargetList;

    Camera mainCamera;
    RaycastHit rcHit;
    RaycastHit lcHit;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        targetObject = null;
        privateTargetObject = null;
        if(allTargets == null)
        {
            allTargets = GameObject.FindGameObjectsWithTag("Targetable").OrderBy(i => Vector3.Distance(transform.position, i.transform.position)).ToList();
        }
        if(allVisibleTargets == null)
        {
            allVisibleTargets = new List<GameObject>();
        }
        if(m_PreviousTargetList == null)
        {
            m_PreviousTargetList = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //get field of view based on character stats
            if(transform.gameObject.GetComponent<CharacterStats>() != null)
            {
                //set field of view; not implemented
                fov = 90;
            }

            //update allVisibleTargets
            allVisibleTargets = new List<GameObject>();
            foreach (var item in allTargets)
            {
                if(item.transform != null && transform != null)
                {
                    //check if item is within line of sight
                    if (Vector3.Angle(item.transform.position - transform.position, transform.forward) <= fov &&
                        Physics.Linecast(transform.position, item.transform.position, out lcHit) && lcHit.collider.transform == item.transform)
                    {
                        allVisibleTargets.Add(item);
                        if (item.GetComponent<CharacterHealth>() != null)
                        {
                            item.GetComponent<CharacterHealth>().m_ShowHealth = true;
                        }
                    }
                    else
                    {
                        if (item.GetComponent<CharacterHealth>() != null)
                        {
                            item.GetComponent<CharacterHealth>().m_ShowHealth = false;
                        }
                    }
                }
            }

            if(allVisibleTargets.Count > 0)
            {
                if (m_PreviousTargetList.Count > 0 && m_PreviousTargetList.Count >= allVisibleTargets.Count)
                {
                    m_PreviousTargetList.Clear();
                }

                if (allVisibleTargets.Count > 0)
                {
                    foreach (var item in allVisibleTargets)
                    {
                        if(item != null)
                        {
                            if (GetTarget() == null)
                            {
                                SetTarget(item.transform);
                            }
                            if (Vector3.Distance(transform.position, item.transform.position) <= Vector3.Distance(transform.position, GetTarget().transform.position) && !m_PreviousTargetList.Contains(item))
                            {
                                SetTarget(item.transform);
                            }
                        }
                    }
                    m_PreviousTargetList.Add(GetTarget().gameObject);
                }
                else if(m_PreviousTargetList != null)
                {
                    m_PreviousTargetList.Clear();
                }
            }
        }
        targetObject = privateTargetObject;
    }

    public void SetTarget(Transform in_Target)
    {
        privateTargetObject = in_Target;
    }

    public Transform GetTarget()
    {
        return privateTargetObject;
    }
}
