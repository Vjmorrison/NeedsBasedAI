using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DataWarehouse {

    public enum DataType
    {
        HUMAN_FIRSTNAMES,
        HUMAN_LASTNAMES,
        MONSTER_NAMES,
        ADJECTIVES,
    }

    private static DataWarehouse s_instance;

    Dictionary<DataType, string[]> m_autogenData;

    public static DataWarehouse Get()
    {
        if (s_instance == null)
        {
            s_instance = new DataWarehouse();
            s_instance.Init();
        }

        return s_instance;
    }

    public void Init()
    {
        m_autogenData = new Dictionary<DataType, string[]>();

        TextAsset[] datafiles = GameManager.Get().m_allDataFiles;

        foreach (TextAsset data in datafiles)
        {
            string[] dataText = data.text.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            m_autogenData.Add(Enums.ParseEnum<DataType>(data.name.ToUpper()), dataText);
        }
    }

    public string GetRandomValue(DataType type)
    {
        return m_autogenData[type][UnityEngine.Random.Range(0, m_autogenData[type].Length)];
    }
}
