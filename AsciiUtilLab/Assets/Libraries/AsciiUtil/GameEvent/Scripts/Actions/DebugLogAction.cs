using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil.GameEvents
{
    [System.Serializable,AddTypeMenu("DebugLog")]
    public class DebugLogAction : IGameEventActionable
    {
        [SerializeField]
        private string logText;
        public void Action()
        {
            AsciiDebug.Log(logText);
        }
    }
}
