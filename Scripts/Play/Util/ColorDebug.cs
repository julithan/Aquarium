using UnityEngine;

public static class ColorDebug
{
    public enum DebugColorType
    {
        white,
        grey,
        black,
        red,
        green,
        blue,
        yellow,
        cyan,
        brown,
    }

    public static void Log(string inStr, DebugColorType inColorType = DebugColorType.green)
    {
#if UNITY_EDITOR
        Debug.LogFormat("<color={0}>{1}</color>", inColorType.ToString(), inStr);
#endif
    }
}
