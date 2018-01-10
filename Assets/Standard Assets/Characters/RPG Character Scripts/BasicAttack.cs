using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

    CharacterHealth targetHealth;
    CharacterStats targetStats;
    List<float> targetStatsList;
    CharacterStats attackerStats;
    List<float> attackerStatsList;
    float attackerDamage;
    AttackType m_AttackType;

    void OnTriggerEnter (Collider col)
    {
        var weaponHolder = transform.parent.parent.gameObject;
        attackerStats = weaponHolder.GetComponent<CharacterStats>();
        attackerStatsList = attackerStats.GetCurrentStats();
        if (attackerStats != null)
        {
            if (attackerStatsList[1] > attackerStatsList[0])
            {
                attackerDamage = attackerStatsList[1] * 2.5f;
                m_AttackType = AttackType.precision;
            }
            else
            {
                attackerDamage = attackerStatsList[0] * 4f;
                m_AttackType = AttackType.power;
            }
        }
        if (col.gameObject.name == "PracticeBox")
        {
            targetStats = col.gameObject.GetComponent<CharacterStats>();
            targetStatsList = targetStats.GetCurrentStats();
            targetHealth = col.gameObject.GetComponent<CharacterHealth>();
            if(targetHealth != null && !targetHealth.isDead)
            {
                if(m_AttackType == AttackType.precision)
                {
                    //precision attacks deal less damage but ignore constitutional defenses
                    targetHealth.TakeDamage(attackerDamage + 500f);
                }
                else
                {
                    //power attacks are reduced (multiplied) by 100% (1) minus 5% (0.05) of, the defender's constitution minus 2.5% (0.025) of the attacker's strength
                    targetHealth.TakeDamage(attackerDamage + 500f * (1 - (0.05f * (attackerStatsList[2] - (0.025f * attackerStatsList[0])))));
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

enum AttackType
{
    precision, power
}
