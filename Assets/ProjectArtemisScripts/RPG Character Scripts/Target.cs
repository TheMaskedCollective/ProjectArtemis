using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public Transform targetObject;
    Transform privateTargetObject;

    float targetObjectIndex = 0;
    float fov = 90;

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
        if(allTargets == null)
        {
            allTargets = GameObject.FindGameObjectsWithTag("Targetable").Where(i => !i.GetComponent<CharacterHealth>().m_IsDead).OrderBy(i => Vector3.Distance(transform.position, i.transform.position)).ToList();
        }
        if(m_PreviousTargetList == null)
        {
            m_PreviousTargetList = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update targets
        SetVisibleTargets();

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if(allVisibleTargets.Count > 0)
            {
                //clear the list if full
                if (m_PreviousTargetList.Count > 0 && m_PreviousTargetList.Count >= allVisibleTargets.Count)
                {
                    m_PreviousTargetList.Clear();
                }
                //Compare each targetable item with others and select closest not previously selected
                GameObject previousItem = null;
                foreach (var item in allVisibleTargets.Where(i => !m_PreviousTargetList.Contains(i)))
                {
                    if (item != null)
                    {
                        //check if null and set if so
                        if (GetTarget() == null)
                        {
                            SetTarget(item.transform);
                        }
                        //check if null and set if so
                        if (previousItem == null)
                        {
                            previousItem = item;
                        }
                        //compare distances and set closest not previously selected
                        if (Vector3.Distance(transform.position, item.transform.position) <= Vector3.Distance(transform.position, previousItem.transform.position))
                        {
                            SetTarget(item.transform);
                        }
                    }
                    previousItem = item;
                }
                m_PreviousTargetList.Add(GetTarget().gameObject);

                //remove selection from previous target
                if (targetObject != null)
                {
                    targetObject.GetComponent<CharacterHealth>().m_IsCurrentTarget = false;
                }
            }
        }
        //set new target and enable selection on target
        targetObject = GetTarget();
        if (targetObject != null)
        {
            var health = targetObject.GetComponent<CharacterHealth>();
            health.m_IsCurrentTarget = true;
        }
    }

    public void SetTarget(Transform in_Target)
    {
        privateTargetObject = in_Target;
    }

    public Transform GetTarget()
    {
        return privateTargetObject;
    }

    public void SetVisibleTargets()
    {
        //update allVisibleTargets
        allVisibleTargets = new List<GameObject>();
        foreach (var item in allTargets.Where(i => !i.GetComponent<CharacterHealth>().m_IsDead))
        {
            if (item.transform != null && transform != null)
            {
                if (item.GetComponentInChildren<Renderer>().IsVisibleFrom(Camera.main))
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
    }
}
