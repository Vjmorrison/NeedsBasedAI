using UnityEngine;
using System.Collections;

public class InteractiveObjectController : MonoBehaviour {

    public InteractiveObject m_objectInstance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        if (m_objectInstance == null)
        {
            m_objectInstance = new InteractiveObject();
            m_objectInstance.m_location = transform.position;
            m_objectInstance.m_rotation = transform.rotation;
        }
    }
}
