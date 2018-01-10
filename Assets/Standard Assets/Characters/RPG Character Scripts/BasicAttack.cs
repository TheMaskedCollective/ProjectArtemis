using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

    CharacterHealth targetHealth;
    CharacterStats targetStats;
    List<float> targetStatsList;
    CharacterStats attackerStats;
    List<float> attackerStatsList;
    float attackerDamage;
    AttackType m_AttackType;
    bool isAttacking = false;
    int updateCount = 0;
    Timer updateTimer;

    void OnTriggerStay (Collider col)
    {
        targetStats = col.gameObject.GetComponent<CharacterStats>();
        if (isAttacking && targetStats != null)
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
            if (targetStats.m_CharacterName == "boxMan")
            {
                targetStats = col.gameObject.GetComponent<CharacterStats>();
                targetStatsList = targetStats.GetCurrentStats();
                targetHealth = col.gameObject.GetComponent<CharacterHealth>();
                if (targetHealth != null && !targetHealth.m_IsDead)
                {
                    if (m_AttackType == AttackType.precision)
                    {
                        //precision attacks deal less damage but ignore constitutional defenses
                        targetHealth.TakeDamage(attackerDamage + 500f, transform.parent.parent.gameObject);
                    }
                    else
                    {
                        //power attacks are reduced (multiplied) by 100% (1) minus 5% (0.05) of, the defender's constitution minus 2.5% (0.025) of the attacker's strength
                        targetHealth.TakeDamage(attackerDamage + 500f * (1 - (0.05f * (attackerStatsList[2] - (0.025f * attackerStatsList[0])))), transform.parent.parent.gameObject);
                    }
                }
            }
            UpdateTimer_Elapsed(this, null);
        }
    }

	// Use this for initialization
	void Start () {
    }

    private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (isAttacking)
        {
            isAttacking = false;
            updateTimer.Dispose();
            updateTimer = null;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            isAttacking = true;
            updateTimer = new Timer(500); //.5 seconds (500 milliseconds)
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.AutoReset = false;
            updateTimer.Start();
        }
    }
}

enum AttackType
{
    precision, power
}
