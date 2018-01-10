using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterHealth : MonoBehaviour {

    float m_StartingHealth;
    float m_CurrentHealth;
    CharacterStats m_CharacterStats;
    List<float> m_CharacterStatsList;
    public bool m_IsDead;
    bool m_Damaged;
    Slider m_HealthSlider;
    GameObject m_Attacker;

	// Use this for initialization
	void Start () {
        m_CharacterStats = GetComponent<CharacterStats>();
        m_CharacterStats.SetBaseStats();
        m_CharacterStatsList = m_CharacterStats.GetCurrentStats();
        m_StartingHealth = m_CharacterStatsList[2] * 100;
        m_CurrentHealth = m_StartingHealth;
        m_HealthSlider = GetComponentInChildren<Slider>();
        if (m_HealthSlider != null)
        {
            m_HealthSlider.maxValue = m_StartingHealth;
            m_HealthSlider.value = m_StartingHealth;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (m_Damaged)
        {
            //show damage frames
        }
        m_Damaged = false;
        //set health bar to face camera
        if(m_HealthSlider != null)
        {
            m_HealthSlider.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void TakeDamage(float amount, GameObject inAttacker) //type and stats
    {
        m_Attacker = inAttacker;

        //subtract damage from health
        m_CurrentHealth -= amount;

        //display changes in health bar
        if (m_HealthSlider != null)
        {
            m_HealthSlider.value = m_CurrentHealth;
        }

        //indicate damage dealt
        m_Damaged = true;

        //check for death
        if(m_CurrentHealth < 1 && !m_IsDead)
        {
            Death();
        }
    }

    public void Death()
    {
        m_IsDead = true;
        if (m_CharacterStats.m_CharacterName != "boxMan")
        {
            Destroy(transform.gameObject);
        }
        //display death results; disable character actions and/or player controls and display UI
        if(m_CharacterStats.m_CharacterName == "boxMan")
        {
            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.AddForce(m_Attacker.transform.forward * 10, ForceMode.Impulse);
            Destroy(m_HealthSlider.transform.parent.gameObject);
        }
    }
}
