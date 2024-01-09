using UnityEngine;

namespace AsciiUtil.Tools
{
    /// <summary>
    /// ログをどこで使ってるのか追いにくいからラップした
    /// </summary>
    public class DebugLog : MonoBehaviour
    {
        public static void Log<T>(T value, GameObject target = null)
        {
            if (target is null)
            {
                Debug.Log(value.ToString());
                return;
            }
            Debug.Log(value.ToString(), target);
        }

        public static void LogError<T>(T value, GameObject target = null)
        {
            if (target is null)
            {
                Debug.LogError(value.ToString());
                return;
            }
            Debug.LogError(value.ToString(), target);
        }
    }
}
