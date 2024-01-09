using UnityEngine;
using UnityEngine.Events;

namespace AsciiUtil
{
    [System.Serializable, CreateAssetMenu(menuName = "AsciiUtil/State/SingleableStateInfo")]
    public class SingleableStateInfo : ScriptableStateInfo
    {
        public override ScriptableStateInfo InitializeStateInfo()
        {
            return this;
        }
        public override ScriptableStateData InitializeState()
        {
            return initialState;
        }
        public override (bool canTransition, ScriptableStateData nextState) CanNextStateTransition(AsciiStateMachine stateMachine, ScriptableStateData currentState)
        {
            foreach (var transition in currentState.TransitionData)
            {
                //遷移フラグが立っていなければ次へ
                if (!transition.trigger.Verify(stateMachine)) continue;

                //trueStateがnullならfalse
                if (transition.trueState is null)
                {
                    Debug.LogError($"{currentState.name} のtrueStateがnullだからEmptyState返したよ");
                    return (false, empty);
                }
                return (true, transition.trueState);
            }
            //どの遷移フラグも立っていなければfalse
            return (false, empty);
        }
    }
}