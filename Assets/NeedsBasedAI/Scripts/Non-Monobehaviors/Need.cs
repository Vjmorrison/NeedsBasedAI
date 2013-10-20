using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Need  {

    /// <summary>
    /// The Current Value of the Need
    /// </summary>
    public float m_currentValue;

    /// <summary>
    /// Ensures access to the current value is restrictred to the listed Minimum and Maximum
    /// </summary>
    public float CurrentValue
    {
        get { return m_currentValue; }
        set 
        {
            if (value > m_maxValue)
            {
                m_currentValue = m_maxValue;
            }
            else if (value < m_minValue)
            {
                m_currentValue = m_minValue;
            }
            else
            {
                m_currentValue = value;
            }
        }
    }
    
    /// <summary>
    /// The amount of value that is decayed per descrete unit of time.
    /// </summary>
    public float m_decayPerUnit;

    /// <summary>
    /// The minimum value the need can be
    /// </summary>
    public float m_minValue;

    /// <summary>
    /// The maximum value the need can be
    /// </summary>
    public float m_maxValue;

    /// <summary>
    /// The public display name of the need
    /// </summary>
    public string m_name;

    /// <summary>
    /// the public description of the need.
    /// </summary>
    public string m_description;

    

    /// <summary>
    /// Basic constructor.  Defines the name and description of the need.
    /// </summary>
    /// <param name="startingValue"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public Need(string name, string description)
    {
        m_name = (string.IsNullOrEmpty(name) ? "DefaultNeedName_" + Guid.NewGuid().ToString() : name);

        m_description = (string.IsNullOrEmpty(description) ? "This is a default description" : description);

        m_minValue = 0;
        m_maxValue = 100;
        CurrentValue = 100;
        m_decayPerUnit = 1;
    }

    /// <summary>
    /// Decays the need based on 
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Decay(float deltaTime)
    {
        Debug.Log(string.Format("Decaying need {0} by {1}",m_name, m_decayPerUnit * deltaTime));
        CurrentValue -= m_decayPerUnit * deltaTime;
        Debug.Log(string.Format("Actual need Value for {0} is {1}", m_name, CurrentValue));
    }

    public void AddValue(float value)
    {
        CurrentValue += value;
    }

    public float GetNormalizedValue()
    {
        return CurrentValue / (float)(m_maxValue - m_minValue);
    }

}
