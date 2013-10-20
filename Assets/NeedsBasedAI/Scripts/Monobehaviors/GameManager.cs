using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public WorldManager m_worldMgr;

    private static GameManager m_instance;
    public static GameManager Get()
    {
        return m_instance;
    }

    public float m_timeRate;

    public readonly float m_MaxDeltaTimeSpan = 720;

    public float m_debugTick;
    public string m_debugTickString;

	// Use this for initialization
	void Start () {
        m_instance = this;

        DontDestroyOnLoad(gameObject);
        //DEBUG
        //LOAD Add New Character Menu
        Application.LoadLevel(1);
        //Loads Main Menu
        //Loads Game
        m_debugTick = 0;
        m_debugTickString = "";
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AdvanceGameTime(float deltaTime)
    {
        float TickTimeLeft = deltaTime;
        while(TickTimeLeft > 0)
        {
            TickTimeLeft -= m_MaxDeltaTimeSpan;
            if (TickTimeLeft > 0)
            {
                print(string.Format("Ticking!!!! {0} hours", m_MaxDeltaTimeSpan));
                m_worldMgr.TickWorlds(m_MaxDeltaTimeSpan);
            }
            else
            {
                print(string.Format("Ticking!!!! {0} hours", m_MaxDeltaTimeSpan + TickTimeLeft));
                m_worldMgr.TickWorlds(m_MaxDeltaTimeSpan + TickTimeLeft);
            }
        }
    }

    void OnGUI()
    {
        if (Debug.isDebugBuild && Application.loadedLevel == 2)
        {
            if (GUI.Button(new Rect(Screen.width - 150, 150, 100, 100), "Tick"))
            {
                AdvanceGameTime(m_debugTick);
            }

            GUI.Label(
            new Rect(
                Screen.width - 150,
                75,
                100,
                50
            ),
            "Hours to Jump");

            float newValue = 0F;
            if(float.TryParse(m_debugTickString, out newValue))
            {
                m_debugTick = newValue;
                m_debugTickString = GUI.TextField(
                    new Rect(
                        Screen.width - 150,
                        0,
                        150,
                        50
                    ),
                    m_debugTick.ToString(), 5);
            }
            else
            {
                m_debugTickString = GUI.TextField(
                    new Rect(
                        Screen.width - 150,
                        0,
                        150,
                        50
                    ),
                    m_debugTickString, 5);
            }
        }
    }

}
