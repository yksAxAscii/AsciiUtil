using UnityEngine;
using AsciiUtil.Tools;

namespace AsciiUtil.StateMachine
{
    /// <summary>
    /// InputActionHundlerのIsTouchを検知するトリガー
    /// </summary>
    [System.Serializable, AddTypeMenu("Input/IsRelease")]
    public class IsReleaseTrigger : IStateTriggerable
    {
        private InputActionHundler inputActionHundler;
        public void Initialize(AsciiStateMachine stateMachine)
        {
           inputActionHundler = GameObject.FindObjectOfType<InputActionHundler>();
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return inputActionHundler.IsRelease.Value;
        }
    }
}