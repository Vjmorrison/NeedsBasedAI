using UnityEngine;
using System.Collections.Generic;

public class WorldManager : MonoBehaviour {

    public List<WorldChunk> m_allChunks;

    public CurrentViewMgr m_currentViewInstance;

    private static WorldManager m_instance;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void TickWorlds(float deltatime)
    {
        foreach (WorldChunk chunk in m_allChunks)
        {
            if (chunk == m_currentViewInstance.m_currentChunk)
            {
                chunk.UpdateChunk(Time.deltaTime, AI_LOD.HIGH);
            }
            else
            {
                chunk.UpdateChunk(Time.deltaTime, AI_LOD.MEDIUM);
            }
        }
    }

    public static bool AddChunk(WorldChunk newChunk)
    {
        if (m_instance.m_allChunks.Contains(newChunk))
        {
            return false;
        }

        m_instance.m_allChunks.Add(newChunk);
        return true;
    }

    void Awake()
    {
        m_instance = this;
        m_allChunks = new List<WorldChunk>(9);
    }
}
