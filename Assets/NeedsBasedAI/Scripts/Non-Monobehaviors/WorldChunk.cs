using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

[System.Serializable]
public class WorldChunk
{
    [SerializeField]
    public List<InteractiveObject> m_allObjects;

    [SerializeField]
    public List<Actor> m_allActors;

    public Vector3 m_location;

    public WorldChunk()
    {
        m_allObjects = new List<InteractiveObject>();
        m_allActors = new List<Actor>();

        m_location = new Vector3();
    }

    public void UpdateChunk(float deltaTime, AI_LOD lod)
    {
        foreach (Actor obj in m_allActors)
        {
            obj.AIUpdate(deltaTime, lod);
        }
    }

    public void AddObject(InteractiveObject newObj)
    {
        if (m_allObjects.Contains(newObj))
        {
            return;
        }
        m_allObjects.Add(newObj);
    }

    public void AddActor(Actor newActor)
    {
        if (m_allActors.Contains(newActor))
        {
            return;
        }
        m_allActors.Add(newActor);
    }

}
