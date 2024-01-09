using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil.StateMachine
{
    [System.Serializable, AddTypeMenu("Rigidbody/AddforceAction")]
    public class AddForceAction : IStateActionable
    {
        [SerializeField]
        private Vector3 direction;
        [SerializeField]
        private float force;
        [SerializeField]
        private ForceMode forceMode;
        [SerializeField]
        private bool initVelocity;
        [System.NonSerialized]
        private Rigidbody rigidBody;
        public void Action(AsciiStateMachine stateMachine)
        {
            if (rigidBody is null)
            {
                if (!stateMachine.TryGetComponent<Rigidbody>(out rigidBody))
                {
                    Debug.LogError($"{stateMachine.name} にRigidbodyがアタッチされてないよ");
                    return;
                }
            }
            if (initVelocity)
            {
                rigidBody.velocity = Vector3.zero;
            }
            rigidBody.AddForce(direction.normalized * force, forceMode);
        }
    }
}