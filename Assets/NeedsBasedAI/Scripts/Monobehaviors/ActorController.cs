using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActorController : MonoBehaviour {

    public Actor m_actorInstance;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        if (m_actorInstance == null)
        {
            m_actorInstance = new Actor();
            m_actorInstance.m_location = transform.position;
            m_actorInstance.m_rotation = transform.rotation;
        }
    }
}
