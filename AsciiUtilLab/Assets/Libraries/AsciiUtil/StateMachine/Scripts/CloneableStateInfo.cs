using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AsciiUtil
{
    [System.Serializable, CreateAssetMenu(menuName = "AsciiUtil/State/CloneableStateInfo")]
    public class CloneableStateInfo : ScriptableStateInfo
    {
        private Dictionary<ScriptableStateData, ScriptableStateData> cloneableStateData;

        public override void SetStateAction(string stateName, ActionType actionType, UnityAction action)
        {
            var state = GetStateData(stateName);
            if (state is null) return;
            state.SetAction(actionType, action);
        }
        protected override ScriptableStateData GetStateData(string stateName)
        {
            foreach (var state in cloneableStateData.Keys)
            {
                if (state.StateName == stateName) return cloneableStateData[state];
            }
            AsciiDebug.LogError($"{stateName} is not found");
            return null;
        }
        public override ScriptableStateInfo InitializeStateInfo()
        {
            return Instantiate(this);
        }
        public override ScriptableStateData InitializeState()
        {
            if (cloneableStateData is null)
            {
                cloneableStateData = new Dictionary<ScriptableStateData, ScriptableStateData>(stateData.Length);
                foreach (var state in stateData)
                {
                    cloneableStateData.Add(state, state.GetClone());
                }
            }
            return cloneableStateData[initialState];
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
                return (true, cloneableStateData[transition.trueState]);
            }
            //どの遷移フラグも立っていなければfalse
            return (false, empty);
        }
    }
}