using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InteractiveObject {

    public List<Action> m_actions;

    public Vector3 m_location;

    public Quaternion m_rotation;

    public InteractiveObject()
    {
        m_actions = new List<Action>();

        m_location = new Vector3();
        m_rotation = new Quaternion();
    }

}
