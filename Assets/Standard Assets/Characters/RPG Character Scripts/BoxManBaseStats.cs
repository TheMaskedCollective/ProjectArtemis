using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Standard_Assets.Characters.ThirdPersonCharacter.Scripts
{
    static class BoxManBaseStats
    {
        public static List<float> GetBaseStats()
        {
            float m_Strength = 1;
            float m_Dexterity = 1;
            float m_Constitution = 10;
            float m_Intelligence = 1;
            float m_Wisdom = 1;
            float m_Charisma = 1;

            List<float> statsList = new List<float> { m_Strength, m_Dexterity, m_Constitution, m_Intelligence, m_Wisdom, m_Charisma };
            return statsList;
        }
    }
}
