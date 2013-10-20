using UnityEngine;
using System.Collections.Generic;
using System;

public class PeopleGenerator {

    private static PeopleGenerator s_instance;

    public static PeopleGenerator Get()
    {
        if (s_instance == null)
        {
            s_instance = new PeopleGenerator();
        }

        return s_instance;
    }

    public PeopleGenerator()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Actor GetDefaultHuman()
    {
        Actor defaultHuman = new Actor();

        defaultHuman.InitDefaultStats();
        defaultHuman.InitDefaultNeeds();
        defaultHuman.InitDefaultUtilities();

        return defaultHuman;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mutationRate"></param>
    /// <returns></returns>
    public Actor GetRandomizedHuman(float mutationRate)
    {
        return BreedHuman(GetDefaultHuman(), GetDefaultHuman(), mutationRate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    /// <returns></returns>
    public Actor BreedHuman(Actor parent1, Actor parent2)
    {
        return BreedHuman(GetDefaultHuman(), GetDefaultHuman(), 0.0f);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    /// <param name="mutationRate"></param>
    /// <returns></returns>
    public Actor BreedHuman(Actor parent1, Actor parent2, float mutationRate)
    {
        Actor newActor = GetDefaultHuman();

        //get parent1 alleles
        foreach (string key in parent1.m_actorStats.Keys)
        {
            Statistic stat = parent1.m_actorStats[key];
            int chosenAlleleIndex = UnityEngine.Random.Range(0, stat.m_statBonuses.Length);
            newActor.m_actorStats[key].m_statBonuses[0] = stat.m_statBonuses[chosenAlleleIndex];
        }
        //get parent2 alleles
        foreach (string key in parent2.m_actorStats.Keys)
        {
            Statistic stat = parent2.m_actorStats[key];
            int chosenAlleleIndex = UnityEngine.Random.Range(0, stat.m_statBonuses.Length);
            newActor.m_actorStats[key].m_statBonuses[0] = stat.m_statBonuses[chosenAlleleIndex];
        }

        if (mutationRate != 0)
        {
            foreach (string key in newActor.m_actorStats.Keys)
            {
                Statistic stat = newActor.m_actorStats[key];

                for (int i = 0; i < stat.m_statBonuses.Length; i++)
                {
                    stat.m_statBonuses[i] += (int)Math.Round(UnityEngine.Random.Range(-mutationRate, mutationRate), 1);
                }
            }


        }

        return newActor;

    }
}
