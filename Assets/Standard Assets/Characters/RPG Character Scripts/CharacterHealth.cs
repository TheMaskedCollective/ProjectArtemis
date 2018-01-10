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
    public bool isDead;
    bool damaged;
    Slider healthSlider;

	// Use this for initialization
	void Start () {
        m_CharacterStats = GetComponent<CharacterStats>();
        m_CharacterStatsList = m_CharacterStats.GetCurrentStats();
        m_StartingHealth = m_CharacterStatsList[2] * 100;
        m_CurrentHealth = m_StartingHealth;
        healthSlider = GetComponentInChildren<Slider>();
        if (healthSlider != null)
        {
            healthSlider.maxValue = m_StartingHealth;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            //show damage frames
        }
        damaged = false;
	}

    public void TakeDamage(float amount) //type and stats
    {
        //subtract damage from health
        m_CurrentHealth -= amount;

        //display changes in health bar
        if (healthSlider != null)
        {
            healthSlider.value = m_CurrentHealth;
        }

        //indicate damage dealt
        damaged = true;

        //check for death
        if(m_CurrentHealth < 1 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        Destroy(transform.gameObject);
        //display death results; disable character actions and/or player controls and display UI
    }
}
