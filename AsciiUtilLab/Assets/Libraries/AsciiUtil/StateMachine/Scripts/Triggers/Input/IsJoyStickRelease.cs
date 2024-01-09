using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Input/Is_JoyStick_Release")]
    public class IsJoyStickRelease : IStateTriggerable
    {
        JoyStick joyStick;
        public void Initialize(AsciiStateMachine stateMachine)
        {
            joyStick = GameObject.FindObjectOfType<JoyStick>();
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return !joyStick.IsInput;
        }
    }
}
