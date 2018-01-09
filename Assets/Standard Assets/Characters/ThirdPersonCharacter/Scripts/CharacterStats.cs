using Assets.Standard_Assets.Characters.ThirdPersonCharacter.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterStats : MonoBehaviour {

    float m_Strength;
    float m_Dexterity;
    float m_Constitution;
    float m_Intelligence;
    float m_Wisdom;
    float m_Charisma;

    float m_Level; //character level

    List<float> m_BaseStatsList; //list of BASE stats as float values in the same order as above
    List<float> m_CurrentStatsList; //list of CURRENT stats as float values in the same order as above

    List<KeyValuePair<StatName, StatModifier>> m_BaseStatsModifiersList; //list of stats and the modifier as key/value pairs

    public ThirdPersonCharacter m_Character { get; private set; }

    // Use this for initialization
    void Start ()
    {
        if (m_Character != null)
        {
            SetBaseStats(m_Character.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(m_Character != null)
        {
            m_CurrentStatsList = GetCurrentStats();
        }
	}

    public void SetBaseStats(string characterName)
    {
        if(characterName == "Max")
        {
            m_BaseStatsList = MaxBaseStats.GetBaseStats();
        }
        else
        {
            throw new System.Exception("Invalid character name; please create a new character base or correct the character identifier.");
        }
    }

    public void AddStatModifier(StatName statName, StatModifier valueDuration)
    {
        m_BaseStatsModifiersList.Add(new KeyValuePair<StatName, StatModifier>(statName, valueDuration));
    }

    public List<float> GetCurrentStats()
    {
        m_Strength = m_BaseStatsList[0] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.strength).Sum(s => s.Value.amount);
        m_Dexterity = m_BaseStatsList[1] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.dexterity).Sum(s => s.Value.amount);
        m_Constitution = m_BaseStatsList[2] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.constitution).Sum(s => s.Value.amount);
        m_Intelligence = m_BaseStatsList[3] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.intelligence).Sum(s => s.Value.amount);
        m_Wisdom = m_BaseStatsList[4] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.wisdom).Sum(s => s.Value.amount);
        m_Charisma = m_BaseStatsList[5] + m_BaseStatsModifiersList.Where(m => m.Key == StatName.charisma).Sum(s => s.Value.amount);
        for (int i = m_BaseStatsModifiersList.Count; i >= 0; i--)
        {
            if (m_BaseStatsModifiersList[i].Value.duration == 0)
            {
                m_BaseStatsModifiersList.RemoveAt(i);
            }
        }
        return new List<float> { m_Strength, m_Dexterity, m_Constitution, m_Intelligence, m_Wisdom, m_Charisma };
    }
}

public enum StatName
{
    strength, dexterity, constitution, intelligence, wisdom, charisma
}
