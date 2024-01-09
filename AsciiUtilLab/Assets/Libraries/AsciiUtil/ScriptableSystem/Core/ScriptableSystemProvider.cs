using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil
{
    public class ScriptableSystemProvider : SingletonMonoBehaviour<ScriptableSystemProvider>
    {
        [SerializeField]
        private ScriptableObject[] systemObjects;
        private List<IScriptableSystemable> scriptableSystems;

        protected override void Awake()
        {
            scriptableSystems = new List<IScriptableSystemable>();
            //システムをセット
            foreach (var systemObject in systemObjects)
            {
                scriptableSystems.Add((IScriptableSystemable)systemObject);
            }
            base.Awake();
        }

        public T GetSystem<T>() where T : IScriptableSystemable
        {
            foreach (var system in scriptableSystems)
            {
                if (system.GetType() == typeof(T))
                {
                    return (T)system;
                }
            }
            Debug.LogError($"{typeof(T)} のSystemObjectがアタッチされてないよ");
            return default(T);
        }
    }
}