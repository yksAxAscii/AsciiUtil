using UnityEngine;
using UnityEngine.Events;

namespace AsciiUtil
{
    public abstract class ScriptableStateInfo : ScriptableObject
    {
        [SerializeField]
        protected ScriptableStateData initialState;
        [SerializeField, Header("ステート一覧")]
        protected ScriptableStateData[] stateData;
        protected ScriptableStateData empty;

        public abstract ScriptableStateInfo InitializeStateInfo();
        public abstract ScriptableStateData InitializeState();
        public virtual void SetStateAction(string stateName, ActionType actionType, UnityAction action)
        {
            var state = GetStateData(stateName);
            if (state is null) return;
            state.SetAction(actionType, action);
        }
        protected virtual ScriptableStateData GetStateData(string stateName)
        {
            foreach (var state in stateData)
            {
                if (state.StateName == stateName) return state;
            }
            AsciiDebug.LogError($"{stateName} is not found");
            return null;
        }
        public abstract (bool canTransition, ScriptableStateData nextState) CanNextStateTransition(AsciiStateMachine stateMachine, ScriptableStateData currentStateData);
    }
}