using UnityEngine;
using UnityEngine.Events;

namespace AsciiUtil
{
    public class AsciiStateMachine : MonoBehaviour
    {
        [SerializeField]
        private ScriptableStateInfo scriptableStateInfo;
        [SerializeField]
        private ScriptableStateData currentState;
        [SerializeField]
        private UnityEvent<ScriptableStateData, ScriptableStateData> onStateChange;

        public void SetStateAction(string stateName, ActionType actionType, UnityAction action)
        {
            scriptableStateInfo.SetStateAction(stateName, actionType, action);
        }

        public void Initialize()
        {
            currentState = scriptableStateInfo.InitializeState();
            TriggerInitialize();
            currentState.EnterAction();
        }

        private void Awake()
        {
            scriptableStateInfo = scriptableStateInfo.InitializeStateInfo();
        }

        private void Start()
        {
            Initialize();
        }

        private void TriggerInitialize()
        {
            foreach (var transition in currentState.TransitionData)
            {
                transition.trigger.Initialize(this);
            }
        }

        private void Update()
        {
            currentState.UpdateAction();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateAction();
        }

        private void LateUpdate()
        {
            //次のステートへの遷移が可能なら遷移
            var value = scriptableStateInfo.CanNextStateTransition(this, currentState);
            if (!value.canTransition) return;
            currentState.ExitAction();
            var previousState = currentState;
            currentState = value.nextState;
            TriggerInitialize();
            currentState.EnterAction();

            //ステート変更時の処理があれば実行
            if (onStateChange is null) return;
            onStateChange.Invoke(currentState, previousState);
        }
    }
}