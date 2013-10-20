using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Actor : InteractiveObject {

    public float m_currentTemperature;
    public readonly float IDEAL_TEMP = 23.0f;

    public float m_age;

    public List<Need> m_needs;

    public List<float> m_utilities;

    public List<UtilityDelegate> m_utilityFunctions;

    public Dictionary<string,Statistic> m_actorStats;

    public List<Trait> m_actorTraits;

    public Actor()
    {
        InitDefaultStats();
        InitDefaultNeeds();
        InitDefaultUtilities();
        InitDefaultTraits();

        m_currentTemperature = IDEAL_TEMP;
    }

    private void InitDefaultTraits()
    {
        m_actorTraits = new List<Trait>();
    }

    public virtual void InitDefaultStats()
    {
        m_actorStats = new Dictionary<string,Statistic>(5);

        Statistic strengthStat = new Statistic(0, 0);
        strengthStat.m_name = "Strength";
        strengthStat.m_shortName = "STR";
        m_actorStats.Add(strengthStat.m_shortName, strengthStat);

        Statistic agilityStat = new Statistic(0, 0);
        agilityStat.m_name = "Agility";
        agilityStat.m_shortName = "AGL";
        m_actorStats.Add(agilityStat.m_shortName, agilityStat);

        Statistic vitalityStat = new Statistic(0, 0);
        vitalityStat.m_name = "Vitality";
        vitalityStat.m_shortName = "VIT";
        m_actorStats.Add(vitalityStat.m_shortName, vitalityStat);

        Statistic perceptionStat = new Statistic(0, 0);
        perceptionStat.m_name = "Perception";
        perceptionStat.m_shortName = "PER";
        m_actorStats.Add(perceptionStat.m_shortName, perceptionStat);

        Statistic charismaStat = new Statistic(0, 0);
        charismaStat.m_name = "Charisma";
        charismaStat.m_shortName = "CHA";
        m_actorStats.Add(charismaStat.m_shortName, charismaStat);
    }

    public virtual void InitDefaultNeeds()
    {
        m_needs = new List<Need>();
        Need foodNeed = new Need("Food", "Requires noureshment");
        Need environmentNeed = new Need("Environment", "Requires protection from cold/heat");
        environmentNeed.m_decayPerUnit = 0;
        Need breedingNeed = new Need("Reproduction", "The desire to reproduce");
        Need entertainmentNeed = new Need("Entertainment", "Requires fun and liesure activities");
        Need sleepNeed = new Need("Sleep", "Requires rest and sleep");
        m_needs.Add(foodNeed);
        m_needs.Add(environmentNeed);
        m_needs.Add(breedingNeed);
        m_needs.Add(entertainmentNeed);
        m_needs.Add(sleepNeed);
    }

    public virtual void InitDefaultUtilities()
    {
        m_utilityFunctions = new List<UtilityDelegate>();
        m_utilityFunctions.Add(FoodUtilityCalc);
        m_utilityFunctions.Add(EnvironmentUtilityCalc);
        m_utilityFunctions.Add(BreedingUtilityCalc);
        m_utilityFunctions.Add(GenericUtilityNeed);
        m_utilityFunctions.Add(GenericUtilityNeed);

        m_utilities = new List<float>();
        for (int i = 0; i < m_needs.Count; i++)
        {
            m_utilities.Add(m_utilityFunctions[i](m_needs[i].GetNormalizedValue()));
        }
    }

    public float GenericUtilityNeed(float normalizedNeedValue)
    {
        return (float)(1 - normalizedNeedValue);
    }

    public float FoodUtilityCalc(float normalizedNeedValue)
    {
        double strengthMod = System.Math.Pow(normalizedNeedValue, (((float)m_actorStats["STR"].GetValue() - Statistic.s_baseValue) / 10) + 2.651f);

        return (float)System.Math.Pow(1 - strengthMod, 4 + (-1 * (((float)m_actorStats["VIT"].GetValue() - Statistic.s_baseValue) / 7)));
    }

    public float EnvironmentUtilityCalc(float normalizedNeedValue)
    {
        double vitalityMod = (((-1.0f) * ((float)m_actorStats["VIT"].GetValue() - Statistic.s_baseValue)) / 6) + 2;

        return -1 * (float)System.Math.Pow(normalizedNeedValue, vitalityMod) + 1;
    }

    public float BreedingUtilityCalc(float normalizedNeedValue)
    {
        if (m_age < 15)
        {
            return 0.0f;
        }

        double vitalityMod = 2 - (((float)m_actorStats["VIT"].GetValue() - Statistic.s_baseValue) / 6);

        return (float)System.Math.Pow(0.9f - normalizedNeedValue, vitalityMod);
    }

    public void ModifyStatAlleles(string statShortName, int fatherBonus, int motherBonus)
    {
        if (!m_actorStats.ContainsKey(statShortName))
        {
            return;
        }

        m_actorStats[statShortName].m_statBonuses[0] += fatherBonus;
        m_actorStats[statShortName].m_statBonuses[1] += motherBonus;
    }

    public void SetStatAlleles(string statShortName, int fatherBonus, int motherBonus)
    {
        if (!m_actorStats.ContainsKey(statShortName))
        {
            return;
        }

        m_actorStats[statShortName].m_statBonuses[0] = fatherBonus;
        m_actorStats[statShortName].m_statBonuses[1] = motherBonus;
    }

    public void AddNeed(Need newNeed, UtilityDelegate utilDelegate)
    {
        m_needs.Add(newNeed);
        m_utilityFunctions.Add(utilDelegate);
        m_utilities.Add(utilDelegate(newNeed.GetNormalizedValue()));
    }

    public void AIUpdate(float deltaTime, AI_LOD lod)
    {
        for (int index = 0; index < m_needs.Count; index++)
        {
            Need aiNeed = m_needs[index];

            if (aiNeed.m_name == "Environment")
            {
                float NewDecayRate = Mathf.Abs(m_currentTemperature - IDEAL_TEMP)/30;
                if(NewDecayRate < 0.5f)
                {
                    aiNeed.m_decayPerUnit = -2;
                }
                else
                {
                    aiNeed.m_decayPerUnit = NewDecayRate;
                }
            }

            if (aiNeed.m_name == "Reproduction")
            {
                if (m_age < 15)
                {
                    aiNeed.m_decayPerUnit = 0;
                }
                else
                {
                    aiNeed.m_decayPerUnit = 0.33f;
                }
            }

            aiNeed.Decay(deltaTime);

            m_utilities[index] = m_utilityFunctions[index](aiNeed.GetNormalizedValue());
        }

    }
}
