using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, CreateAssetMenu(menuName = "ScriptableSystem/ObjectSetSystem")]
    public class ObjectSetSystem : ScriptableObject, IScriptableSystemable
    {
        private Dictionary<string, GameObject> objectDictionary;

        /// <summary>
        /// オブジェクトをセットします
        /// </summary>
        /// <param name="targetObject"></param>
        public void SetObject(SetableObject targetObject)
        {
            if (objectDictionary is null)
            {
                objectDictionary = new Dictionary<string, GameObject>();
            }
            if (objectDictionary.ContainsKey(targetObject.ObjectName))
            {
                AsciiDebug.LogError($"{targetObject.ObjectName} is already exist");
                return;
            }
            objectDictionary.Add(targetObject.ObjectName, targetObject.gameObject);
        }

        /// <summary>
        /// 指定のオブジェクトを取得します
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns> <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public GameObject GetAnySetableObject(string objectName)
        {
            if (objectDictionary is null)
            {
                AsciiDebug.LogError($"objectDictionary is null");
                return null;
            }
            if (!objectDictionary.ContainsKey(objectName))
            {
                AsciiDebug.LogError($"{objectName} is not found");
                return null;
            }
            return objectDictionary[objectName];
        }

        /// <summary>
        /// セットされているオブジェクトを破棄します
        /// </summary> <summary>
        /// 
        /// </summary>
        public void ClearSetableObjects()
        {
            objectDictionary.Clear();
        }
    }
}
