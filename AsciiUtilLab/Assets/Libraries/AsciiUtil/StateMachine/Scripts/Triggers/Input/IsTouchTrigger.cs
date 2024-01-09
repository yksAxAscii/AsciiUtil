using UnityEngine;
using AsciiUtil.Tools;

namespace AsciiUtil.StateMachine
{
    /// <summary>
    /// InputActionHundlerのIsTouchを検知するトリガー
    /// </summary>
    [System.Serializable, AddTypeMenu("Input/IsTouch")]
    public class IsTouchTrigger : IStateTriggerable
    {
        private InputActionHundler inputActionHundler;
        public void Initialize(AsciiStateMachine stateMachine)
        {
           inputActionHundler = GameObject.FindObjectOfType<InputActionHundler>();
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return inputActionHundler.IsTouch.Value;
        }
    }
}