using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Action {

    [SerializeField]
    public Dictionary<string, float> m_needValues;

    private Dictionary<Type, int> m_requirements;

    private Dictionary<Type, int> m_results;

    private float m_duration;

    public string m_name;

    public string m_description;

    public Action(string name, string description)
    {
        m_name = (string.IsNullOrEmpty(name) ? "DefaultActionName_" + Guid.NewGuid().ToString() : name);

        m_description = (string.IsNullOrEmpty(description) ? "This is a default description" : description);

        m_needValues = new Dictionary<string, float>();
        m_requirements = new Dictionary<Type, int>();
        m_results = new Dictionary<Type, int>();
    }

    public bool AddNeedValue(string needName, float modifierValue)
    {
        if (m_needValues.ContainsKey(needName))
        {
            return false;
        }

        m_needValues.Add(needName, modifierValue);
        return true;
    }

    public bool AddRequirement(InteractiveObject requiredObject, int amount)
    {
        if (m_requirements.ContainsKey(requiredObject.GetType()))
        {
            return false;
        }

        m_requirements.Add(requiredObject.GetType(), amount);
        return true;
    }

    public bool AddResult(InteractiveObject resultObject, int amount)
    {
        if (m_results.ContainsKey(resultObject.GetType()))
        {
            return false;
        }

        m_results.Add(resultObject.GetType(), amount);
        return true;
    }

    public List<InteractiveObject> GetResults()
    {
        List<InteractiveObject> returnObjects = new List<InteractiveObject>();

        foreach (Type key in m_results.Keys)
        {
            for (int i = 0; i < m_results[key]; i++)
            {
                returnObjects.Add(Activator.CreateInstance(key) as InteractiveObject);
            }
        }

        return returnObjects;
    }

    public Dictionary<Type, int> GetRequirements()
    {
        return m_requirements;
    }
}
