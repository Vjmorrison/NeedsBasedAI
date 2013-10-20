using UnityEngine;
using System.Collections;

public class CurrentViewMgr : MonoBehaviour {

    public WorldChunk m_currentChunk;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_currentChunk != null)
        {
            m_currentChunk.UpdateChunk(Time.deltaTime, AI_LOD.HIGH);
        }
	}
}
