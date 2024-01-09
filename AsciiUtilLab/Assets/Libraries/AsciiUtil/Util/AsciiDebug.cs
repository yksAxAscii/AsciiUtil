using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AsciiDebug
{
    /// <summary>
    /// ログをエディタのコンソールに表示
    /// </summary>
    /// <param name="messages"></param>
    public static void Log(params object[] messages)
    {
        var message = string.Join(",", messages.Select(FormatObject));
        Debug.Log(message);
    }

    /// <summary>
    /// ログをエラーとしてエディタのコンソールに表示
    /// </summary>
    /// <returns></returns>
    public static void LogError(params object[] messages)
    {
        var message = string.Join(",", messages.Select(FormatObject));
        Debug.LogError(message);
    }
    private static string FormatObject(object o)
    {
        if (o is null)
        {
            return "(null)";
        }

        if (o as string == string.Empty)
        {
            return "(empty)";
        }

        return o.ToString();
    }
}