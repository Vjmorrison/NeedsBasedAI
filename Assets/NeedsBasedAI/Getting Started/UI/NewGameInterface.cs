using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class NewGameInterface : MonoBehaviour {

    public GUIStyle m_style;

    public Vector2 m_charStatSize = new Vector2(300,300);
    public Vector2 m_charStatOffset;

    public int buttonFontSize = 12;
    public int textboxFontSize = 12;
    public int boxFontSize = 12;

    public int m_maxPoints = 30;
    public int currentPoints = 0;

    public Statistic[] stats;

    public string[,] tempvalues;

	// Use this for initialization
	void Start () {
        stats = new Statistic[5];
        stats[0] = new Statistic(0, 0);
        stats[0].m_name = "Strength";
        stats[0].m_shortName = "STR";

        stats[1] = new Statistic(0, 0);
        stats[1].m_name = "Agility";
        stats[1].m_shortName = "AGL";

        stats[2] = new Statistic(0, 0);
        stats[2].m_name = "Vitality";
        stats[2].m_shortName = "VIT";

        stats[3] = new Statistic(0, 0);
        stats[3].m_name = "Perception";
        stats[3].m_shortName = "PER";

        stats[4] = new Statistic(0, 0);
        stats[4].m_name = "Charisma";
        stats[4].m_shortName = "CHA";

        tempvalues = new string[5, 2];

	}
	
	// Update is called once per frame
	void Update () {

        
	}

    void OnGUI()
    {
        GUI.skin.button.fontSize = buttonFontSize;
        GUI.skin.box.fontSize = boxFontSize;
        GUI.skin.textField.fontSize = textboxFontSize;
        GUI.skin.textField.alignment = TextAnchor.MiddleCenter;

        GUI.skin.label.fontSize = boxFontSize;

        Rect CharacterStatRECT = new Rect(
            0 + (Screen.width * m_charStatOffset.x),
            0 + (Screen.height * m_charStatOffset.y),
            (Screen.width * m_charStatSize.x), 
            (Screen.height * m_charStatSize.y)
            );

        GUI.BeginGroup(CharacterStatRECT);
        GUI.Box(new Rect(0, 0, m_charStatSize.x, m_charStatSize.y), "Sample Actor");

        for (int i = 0; i < stats.Length; i++)
        {
            string Stat0AsString = (tempvalues[i, 0] == null ? stats[i].m_statBonuses[0].ToString() : tempvalues[i, 0]);
            string Stat1AsString = (tempvalues[i, 1] == null ? stats[i].m_statBonuses[1].ToString() : tempvalues[i, 1]);

            RenderStat(i+1, stats[i].m_shortName + " (♂)", stats[i].m_shortName + " (♀)", ref Stat0AsString, ref Stat1AsString);

            int tempVal;
            if (int.TryParse(Stat0AsString, out tempVal))
            {
                stats[i].m_statBonuses[0] = tempVal;
                tempvalues[i, 0] = null;
            }
            else
            {
                tempvalues[i, 0] = Stat0AsString;
            }

            if (int.TryParse(Stat1AsString, out tempVal))
            {
                stats[i].m_statBonuses[1] = tempVal;
                tempvalues[i, 1] = null;
            }
            else
            {
                tempvalues[i, 1] = Stat1AsString;
            }
        }
        UpdateCurrentPoints();
        GUI.Label(
            new Rect(
                (m_charStatSize.x / 5) * 0,
                (m_charStatSize.y / 6) * 6 - (textboxFontSize + 5),
                m_charStatSize.x,
                boxFontSize + 10
            ),
            "Points Left:  " + (m_maxPoints - currentPoints).ToString());
        //RenderStrength();

        GUI.EndGroup();

        GUI.BeginGroup(new Rect(
            Screen.width / 2 - 50,
            Screen.height / 2 - 50,
            100, 100)
            );

        var items = (from string value in tempvalues
                    where !string.IsNullOrEmpty(value)
                     select value).ToList();

        if (m_maxPoints - currentPoints >= 0 && items.Count <= 0)
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "START"))
            {
                NewGame();
            }
        }

        GUI.EndGroup();
    }

    public void UpdateCurrentPoints()
    {

        int sum = 0;
        foreach (Statistic stat in stats)
        {
            foreach (int value in stat.m_statBonuses)
            {
                if (value > 5)
                {
                    sum += value + (int)Math.Ceiling((float)value/2);
                }
                else
                {
                    sum += value;
                }
            }
        }
        currentPoints = sum;
    }

    public void RenderStat(int yIndex, string labelA, string labelB, ref string valueA, ref string valueB)
    {
        GUI.Label(
            new Rect(
                (m_charStatSize.x / 5) * 0,
                (m_charStatSize.y / 6) * yIndex - (textboxFontSize + 5),
                m_charStatSize.x / 5,
                boxFontSize + 10
            ),
            labelA);

        valueA = GUI.TextField(
            new Rect(
                (m_charStatSize.x / 5) * 0,
                (m_charStatSize.y / 6) * yIndex,
                m_charStatSize.x / 5,
                textboxFontSize + 5
            ),
            valueA, 5);

        GUI.Label(
            new Rect(
                (m_charStatSize.x / 5) * 1,
                (m_charStatSize.y / 6) * yIndex - (textboxFontSize + 5),
                m_charStatSize.x / 5,
                boxFontSize + 10
            ),
            labelB);

        valueB = GUI.TextField(
            new Rect(
                (m_charStatSize.x / 5) * 1,
                (m_charStatSize.y / 6) * yIndex,
                m_charStatSize.x / 5,
                textboxFontSize + 5
            ),
            valueB, 5);
    }

    public void NewGame()
    {
        Actor newActor = new Actor();

        newActor.m_actorStats = stats.ToDictionary<Statistic, string>(x => x.m_shortName);

        WorldChunk newChunk = new WorldChunk();
        newChunk.AddActor(newActor);

        GameManager.Get().m_worldMgr.AddChunk(newChunk);

        Application.LoadLevel(2);
    }
}
