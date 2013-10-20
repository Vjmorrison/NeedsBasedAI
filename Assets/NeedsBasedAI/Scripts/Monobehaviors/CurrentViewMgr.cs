using UnityEngine;
using System.Collections;

public class CurrentViewMgr : MonoBehaviour {

    public WorldChunk m_currentChunk;

    public bool isDebug = true;

    public Rect windowRect = new Rect(20, 20, 170, 120);
    public Rect peopleWindowRect = new Rect(20, 120, 520, 100);

    public Rect familyTreeWindowRect = new Rect(20, 250, 520, 100);

    public Actor chosenActor = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnGUI()
    {
        if (isDebug)
        {
            windowRect = GUI.Window(0, windowRect, MoveWindow, "Debug Window");

            peopleWindowRect = GUI.Window(1, peopleWindowRect, PeopleWindow, "People List Window");

            if (chosenActor != null)
            {
                familyTreeWindowRect = GUI.Window(2, familyTreeWindowRect, FamiliyTreeWindow, "Family Tree: " + chosenActor.m_firstName + " " + chosenActor.m_familyName);
            }
        }
    }

    void MoveWindow(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 150, 20), "Generate Default Human"))
        {
            Actor newActor = PeopleGenerator.Get().GetDefaultHuman();
            GameManager.Get().m_worldMgr.m_allChunks[0].AddActor(newActor);
        }

        if (GUI.Button(new Rect(10, 40, 150, 20), "Generate Random Human"))
        {
            Actor newActor = PeopleGenerator.Get().GetRandomizedHuman(8.0f);
            GameManager.Get().m_worldMgr.m_allChunks[0].AddActor(newActor);
        }

        if (GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors.Count > 1)
        {
            if (GUI.Button(new Rect(10, 60, 150, 20), "Breed Two Humans"))
            {
                int parent1Index = 0;
                int parent2Index = 0;
                while (parent1Index == parent2Index)
                {
                    parent1Index = UnityEngine.Random.Range(0, GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors.Count);
                    parent2Index = UnityEngine.Random.Range(0, GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors.Count);
                }

                Debug.Log(string.Format("Breeding Indexes {0} and {1}", parent1Index, parent2Index));

                Actor newActor = PeopleGenerator.Get().BreedHuman(GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors[parent1Index],
                    GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors[parent2Index]);
                GameManager.Get().m_worldMgr.m_allChunks[0].AddActor(newActor);
            }
        }

        if (GUI.Button(new Rect(10, 80, 150, 20), "Remove ALL Actors"))
        {
            GameManager.Get().m_worldMgr.m_allChunks[0].m_allActors.Clear();
        }
            

        GUI.DragWindow();
    }

    void PeopleWindow(int windowID)
    {
        Rect labelPOS = new Rect(10, 20, peopleWindowRect.width, 20);
        foreach (Actor human in m_currentChunk.m_allActors)
        {
            string StatString = "";
            foreach (Statistic stat in human.m_actorStats.Values)
            {
                StatString += string.Format("{0}: {1}, ", stat.m_shortName, stat.GetValue());
            }

            GUI.Label(labelPOS, human.m_firstName + " " + human.m_familyName + " [" + StatString + "]");
            if (GUI.Button(new Rect(labelPOS.x + 375, labelPOS.y, 120, labelPOS.height), "View Family Tree"))
            {
                Debug.Log(string.Format("Generation-{0}: {1} {2}", 0, human.m_firstName, human.m_familyName));
                LogParents(human, 1);
            }
            labelPOS.y += 20;
        }

        peopleWindowRect.height = labelPOS.y + 20;

        GUI.DragWindow();
    }

    void FamiliyTreeWindow(int windowID)
    {
        
        GUI.DragWindow();
    }

    void LogParents(Actor child, int generation)
    {
        if (child.parent1 != null)
        {
            Debug.Log(string.Format("Generation-{0}: {1} {2}", generation, child.parent1.m_firstName ,child.parent1.m_familyName));
            LogParents(child.parent1, generation + 1);
        }

        if (child.parent2 != null)
        {
            Debug.Log(string.Format("Generation-{0}: {1} {2}", generation, child.parent2.m_firstName, child.parent2.m_familyName));
            LogParents(child.parent2, generation + 1);
        }
    }
}
