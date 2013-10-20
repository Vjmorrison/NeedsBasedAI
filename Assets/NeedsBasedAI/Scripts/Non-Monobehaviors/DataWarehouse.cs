using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DataWarehouse {

    private static DataWarehouse s_instance;

    public static DataWarehouse Get()
    {
        if (s_instance == null)
        {
            s_instance = new DataWarehouse();
        }

        return s_instance;
    }

    public void Init()
    {
        TextAsset[] allData = Resources.FindObjectsOfTypeAll(typeof(TextAsset)).Select<UnityEngine.Object, TextAsset>(x => x as TextAsset).ToArray();
    }
}
