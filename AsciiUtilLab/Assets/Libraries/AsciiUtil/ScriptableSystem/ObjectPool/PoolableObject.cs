using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsciiUtil
{
    public class PoolableObject : MonoBehaviour
    {
        [SerializeField]
        private string objectKey;
        public string ObjectKey => objectKey;
        [SerializeField]
        private PoolableObjectType objectType;
        public PoolableObjectType ObjectType => objectType;
        [SerializeField]
        private UnityEvent rentAction;
        public UnityEvent RentAction { set => rentAction = value; }
        [SerializeField]
        private UnityEvent releaseAction;
        public UnityEvent ReleaseAction { set => releaseAction = value; }
        private Transform originTransform;
        public Transform OriginTransform => originTransform;

        private void Awake()
        {
            originTransform = transform;
        }

        private void OnEnable()
        {
            rentAction.Invoke();
        }

        private void OnDisable()
        {
            releaseAction.Invoke();
        }
    }
    public enum PoolableObjectType
    {
        OBJECT,
        PARTICLE,
        UI,
    }
}
