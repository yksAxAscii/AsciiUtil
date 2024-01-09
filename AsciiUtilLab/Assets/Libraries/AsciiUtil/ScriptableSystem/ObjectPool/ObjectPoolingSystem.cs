using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsciiUtil.ScriptableSystem;

namespace AsciiUtil.ScriptableSystem
{
    //TODO : たくさん使いそうなものをあらかじめ非同期で生成しておく処理作りたい
    //       使っていない期間が長いオブジェクトを非同期で削除する処理作りたい
    [System.Serializable, CreateAssetMenu(menuName = "ScriptableSystem/ObjectPoolingSystem")]
    public class ObjectPoolingSystem : ScriptableObject, IScriptableSystemable
    {
        [SerializeField]
        private string rootName = "PoolRoot";
        [System.NonSerialized]
        private GameObject root;
        [System.NonSerialized]
        Dictionary<string, GameObject> poolableParents = new Dictionary<string, GameObject>();

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
            if (parent is null)
            {
                if (root is null)
                {
                    root = new GameObject(rootName);
                }
                parent = new GameObject(targetObject.ObjectKey);
                parent.transform.SetParent(root.transform);
                poolableParents.Add(targetObject.ObjectKey, parent);
            }

            //プールに非アクティブなオブジェクトがあれば返す
            //なければ作る
            var searchResult = SearchDisablePoolableObject(parent);
            if (searchResult.isSuccess)
            {
                searchResult.value.gameObject.SetActive(true);
                return searchResult.value.gameObject;
            }
            return Instantiate(targetObject.gameObject, parent.transform);
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
            return SearchTargetParent(targetObject.ObjectKey)
            .GetComponentsInChildren<PoolableObject>();
        }

        /// <summary>
        /// 指定したオブジェクトに最も近いアクティブなPoolableObjectを返す
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="targetObject"></param>
        /// <param name="originTransform"></param>
        /// <returns></returns>
        public (bool isSuccess, PoolableObject targetObject) SearchClosestObject(PoolableObject targetObject, Transform originTransform)
        {
            PoolableObject value = null;
            float minDistance = float.MaxValue;
            var objects = GetAllActiveObjects(targetObject);

            if (objects.Length <= 0) return (false, null);

            foreach (var obj in objects)
            {
                float distance = Vector2.Distance(originTransform.position, obj.transform.position);
                if (distance >= minDistance) continue;
                value = obj;
                minDistance = distance;
            }
            return (true, value);
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
            var objects = parent.GetComponentsInChildren<PoolableObject>(true);
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

    }
}
