using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AsciiUtil
{
    public class ObjectPooler : MonoBehaviour
    {
        Dictionary<string, GameObject> poolableParents;
        Dictionary<GameObject, List<PoolableObject>> poolableObjects;

        private void Awake()
        {
            poolableParents = new Dictionary<string, GameObject>();
            poolableObjects = new Dictionary<GameObject, List<PoolableObject>>();
        }
        /// <summary>
        /// targetObjectのプールの中から非アクティブな物があれば渡す
        /// 無ければ生成して渡す
        /// </summary>
        /// <param name="targetObject"></param>
        /// <returns></returns>
        public GameObject Rent(PoolableObject targetObject)
        {
            //プールの親があれば拾ってくる
            //なければ作る
            GameObject parent = SearchTargetParent(targetObject.ObjectKey);
            if (parent == null)
            {
                parent = new GameObject(targetObject.ObjectKey);
                poolableObjects.Add(parent, new List<PoolableObject>());
                //ターゲットがUIならParentにCanvasを追加
                if (targetObject.ObjectType == PoolableObjectType.UI)
                {
                    var canvas = parent.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                }
                parent.transform.SetParent(transform);
                poolableParents.Add(targetObject.ObjectKey, parent);
            }

            //プールに非アクティブなオブジェクトがあれば返す
            var searchResult = SearchDisablePoolableObject(parent);
            if (searchResult.isSuccess)
            {
                searchResult.value.gameObject.SetActive(true);
                return searchResult.value.gameObject;
            }
            //なければ作って返す
            var obj = Instantiate(targetObject.gameObject, parent.transform);
            poolableObjects[parent].Add(obj.GetComponent<PoolableObject>());
            return obj;
        }

        /// <summary>
        /// オブジェクトの返還
        /// </summary>
        /// <param name="targetObject"></param>
        public void Release(PoolableObject targetObject)
        {
            //非アクティブにしてプールに戻し、transformを初期化
            targetObject.gameObject.SetActive(false);
            targetObject.gameObject.transform.position = targetObject.OriginTransform.position;
            targetObject.gameObject.transform.rotation = targetObject.OriginTransform.rotation;
            targetObject.gameObject.transform.localScale = targetObject.OriginTransform.localScale;
        }

        /// <summary>
        /// 指定したPoolableObjectを全て変換
        /// </summary>
        /// <param name="targetObject"></param>
        public void ReleaseAll(PoolableObject targetObject)
        {
            if (SearchTargetParent(targetObject.ObjectKey) == null) return;

            foreach (var obj in GetAllActiveObjects(targetObject))
            {
                Release(obj);
            }
        }

        /// <summary>
        /// アクティブな指定されたPoolableObjectを全て返す
        /// </summary>
        /// <param name="targetObject"></param>
        /// <returns></returns>
        public PoolableObject[] GetAllActiveObjects(PoolableObject targetObject)
        {
            var parent = SearchTargetParent(targetObject.ObjectKey);
            if (parent == null) return new PoolableObject[0];

            var targetObjects = poolableObjects[parent];
            var targetActiveObjects = targetObjects.Where(x => x.gameObject.activeSelf).ToArray();
            return targetActiveObjects;
        }

        public PoolableObject[] GetAllActiveObjects(string targetObjectKey)
        {
            var parent = SearchTargetParent(targetObjectKey);
            if (parent == null) return new PoolableObject[0];

            var targetObjects = poolableObjects[parent];
            var targetActiveObjects = targetObjects.Where(x => x.gameObject.activeSelf).ToArray();
            return targetActiveObjects;
        }

        private Dictionary<PoolableObject, IApplyDamagable> iapllyDamagableDictionary = new Dictionary<PoolableObject, IApplyDamagable>();
        public IApplyDamagable[] GetAllActiveIApplyDamagable(PoolableObject targetObject)
        {
            var objects = GetAllActiveObjects(targetObject);
            if (objects.Length <= 0) return new IApplyDamagable[0];
            foreach (var obj in objects)
            {
                if (iapllyDamagableDictionary.ContainsKey(obj)) continue;
                iapllyDamagableDictionary.Add(obj, obj.GetComponent<IApplyDamagable>());
            }
            var value = iapllyDamagableDictionary.Where(x => x.Key.gameObject.activeSelf).Select(x => x.Value).ToArray();
            return value;
        }

        /// <summary>
        /// 範囲内で最も近いアクティブな指定されたPoolableObjectを返す
        /// </summary>
        /// <param name="targetObject"></param>
        /// <returns></returns> <summary>
        public PoolableObject GetActiveAnyRangeObject(PoolableObject targetObject, Vector3 originPosition, float range)
        {
            var objects = GetAllActiveObjects(targetObject);
            if (objects.Length <= 0) return null;

            PoolableObject value = null;
            float minDistance = float.MaxValue;
            foreach (var obj in objects)
            {
                float distance = (originPosition - obj.transform.position).sqrMagnitude;
                if (distance >= minDistance) continue;
                value = obj;
                minDistance = distance;
            }
            if (value == null)
            {
                AsciiDebug.LogError("オブジェクトが見つからなかったよ", "ObjectKey:", targetObject.ObjectKey);
            }
            return value;
        }
        /// <summary>
        /// 範囲内で最も近いアクティブなIApplyDamagableを返す
        /// </summary>
        /// <param name="targetObject"></param>
        /// <returns></returns> <summary>
        public IApplyDamagable GetActiveAnyRangeIApplyDamagable(PoolableObject targetObject, Vector3 originPosition, float range)
        {
            var objects = GetAllActiveObjects(targetObject);
            if (objects.Length <= 0) return null;

            PoolableObject value = null;
            float minDistance = float.MaxValue;
            foreach (var obj in objects)
            {
                float distance = (originPosition - obj.transform.position).sqrMagnitude;
                if (distance >= minDistance) continue;
                value = obj;
                minDistance = distance;
            }
            if (value == null)
            {
                AsciiDebug.LogError("オブジェクトが見つからなかったよ", "ObjectKey:", targetObject.ObjectKey);
            }
            if (!iapllyDamagableDictionary.ContainsKey(value))
            {
                iapllyDamagableDictionary.Add(value, value.GetComponent<IApplyDamagable>());
            }
            return iapllyDamagableDictionary[value];
        }

        public int GetActiveObjectCount(PoolableObject targetObject)
        {
            return GetAllActiveObjects(targetObject).Length;
        }

        /// <summary>
        /// 指定したオブジェクトに最も近いアクティブなPoolableObjectを返す
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="targetObject"></param>
        /// <param name="originTransform"></param>
        /// <returns></returns>
        public PoolableObject FindClosestObject(PoolableObject targetObject, Transform originTransform)
        {
            PoolableObject value = null;
            float minDistance = float.MaxValue;
            var objects = GetAllActiveObjects(targetObject);

            if (objects.Length <= 0)
            {
                AsciiDebug.LogError("指定されたオブジェクトのプールにオブジェクトがないよ", "ObjectKey:", targetObject.ObjectKey);
                return null;
            }

            foreach (var obj in objects)
            {
                float distance = (originTransform.position - obj.transform.position).sqrMagnitude;
                if (distance >= minDistance) continue;
                value = obj;
                minDistance = distance;
            }
            if (value == null)
            {
                AsciiDebug.LogError("オブジェクトが見つからなかったよ", "ObjectKey:", targetObject.ObjectKey);
            }
            return value;
        }

        /// <summary>
        /// 指定したオブジェクトに最も近いアクティブなPoolableObjectを返す
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="targetObject"></param>
        /// <param name="originTransform"></param>
        /// <returns></returns>
        public PoolableObject FindClosestObject(string targetObjectKey, Transform originTransform)
        {
            PoolableObject value = null;
            float minDistance = float.MaxValue;
            var objects = GetAllActiveObjects(targetObjectKey);

            if (objects.Length <= 0)
            {
                AsciiDebug.LogError("指定されたオブジェクトのプールにオブジェクトがないよ", "ObjectKey:", targetObjectKey);
                return null;
            }

            foreach (var obj in objects)
            {
                float distance = (originTransform.position - obj.transform.position).sqrMagnitude;
                if (distance >= minDistance) continue;
                value = obj;
                minDistance = distance;
            }
            if (value == null)
            {
                AsciiDebug.LogError("オブジェクトが見つからなかったよ", "ObjectKey:", targetObjectKey);
            }
            return value;
        }

        /// <summary>
        /// 子オブジェクトの中から非アクティブなPoolableObjectを探して返す
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private (bool isSuccess, PoolableObject value) SearchDisablePoolableObject(GameObject parent)
        {
            PoolableObject value = null;
            var objects = poolableObjects[parent];
            foreach (var obj in objects)
            {
                if (obj.gameObject.activeSelf) continue;
                value = obj;
                return (true, value);
            }
            return (false, null);
        }

        /// <summary>
        /// 指定のobjectKeyの親オブジェクトを探して返す
        /// </summary>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        private GameObject SearchTargetParent(string objectKey)
        {
            if (!poolableParents.ContainsKey(objectKey))
            {
                return null;
            }
            return poolableParents[objectKey];
        }

        /// <summary>
        /// 虚偽null回避用判定処理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private bool IsNull(GameObject gameObject)
        {
            var unityObj = gameObject as UnityEngine.Object;
            if (!object.ReferenceEquals(unityObj, null))
            {
                return unityObj == null;
            }
            else
            {
                return gameObject == null;
            }
        }
    }

}
