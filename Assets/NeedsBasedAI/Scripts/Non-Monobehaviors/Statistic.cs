using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[System.Serializable]
public class Statistic
{

    public string m_name;

    public string m_shortName;

    public readonly static int s_baseValue = 10;

    public int[] m_statBonuses;

    public Statistic(int motherBonus, int fatherBonus)
    {
        m_statBonuses = new int[2];
        m_statBonuses[0] = motherBonus;
        m_statBonuses[1] = fatherBonus;
    }

    public int GetValue()
    {
        if (m_statBonuses.All(value => value > 0))
        {
            return Statistic.s_baseValue + m_statBonuses.Max();
        }

        if (m_statBonuses.All(value => value < 0))
        {
            return Statistic.s_baseValue + m_statBonuses.Min();
        }

        if (m_statBonuses.Any(value => value == 0))
        {
            int[] adjustedBonusValues = new int[m_statBonuses.Length];

            for (int index = 0; index < m_statBonuses.Length; index++)
            {
                if (m_statBonuses[index] != 0 && Math.Abs(m_statBonuses[index]) <= 5)
                {
                    adjustedBonusValues[index] = m_statBonuses[index] / Math.Abs(m_statBonuses[index]);
                }
                else
                {
                    adjustedBonusValues[index] = m_statBonuses[index];
                }
            }

            return Statistic.s_baseValue + adjustedBonusValues.Sum();
        }

        return Statistic.s_baseValue + m_statBonuses.Sum();        
    }

}
