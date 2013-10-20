using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;

public enum AI_LOD
{
    [Description("No AI calculations")]
    NONE = 0,
    [Description("High Level Of Detail")]
    HIGH = 1,
    [Description("High Level Of Detail")]
    MEDIUM = 2,
    [Description("High Level Of Detail")]
    LOW = 3,
    [Description("High Level Of Detail")]
    MIN = 4
}

public static class Enums
{
    public static T ParseEnum<T>(string enumString)
    {
        try
        {
            T enumValue = (T)Enum.Parse(typeof(T), enumString);
            return enumValue;
        }
        catch
        {
            return default(T);
        }
    }
}
