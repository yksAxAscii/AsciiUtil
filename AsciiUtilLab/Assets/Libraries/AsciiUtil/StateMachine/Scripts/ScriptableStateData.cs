using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace AsciiUtil
{
    [CreateAssetMenu(menuName = "AsciiUtil/State/ScriptableStateData")]
    [System.Serializable]
    public class ScriptableStateData : ScriptableObject
    {
        [SerializeField]
        private string stateName;
        public string StateName => stateName;
        [SerializeField]
        private TransitionData[] transitionData;
        public TransitionData[] TransitionData => transitionData;
        [System.NonSerialized]
        private UnityAction onEnterAction;
        [System.NonSerialized]
        private UnityAction onUpdateAction;
        [System.NonSerialized]
        private UnityAction onFixedUpdateAction;
        [System.NonSerialized]
        private UnityAction onExitAction;
        [System.NonSerialized]
        private Dictionary<ActionType, UnityAction> actionDictionary;
        public void SetAction(ActionType actionType, UnityAction action)
        {
            if (actionDictionary is null)
            {
                actionDictionary = new Dictionary<ActionType, UnityAction>()
                {
                    {ActionType.Enter, onEnterAction},
                    {ActionType.Update, onUpdateAction},
                    {ActionType.FixedUpdate, onFixedUpdateAction},
                    {ActionType.Exit, onExitAction}
                };
            }
            if (!actionDictionary.ContainsKey(actionType))
            {
                AsciiDebug.LogError($"{actionType} is not found");
                return;
            }
            actionDictionary[actionType] += action;
        }

        public ScriptableStateData GetClone()
        {
            return Instantiate(this);
        }

        public void EnterAction()
        {
            if (actionDictionary is null) return;
            actionDictionary[ActionType.Enter]?.Invoke();
        }

        public void UpdateAction()
        {
            if (actionDictionary is null) return;
            actionDictionary[ActionType.Update]?.Invoke();
        }

        public void FixedUpdateAction()
        {
            if (actionDictionary is null) return;
            actionDictionary[ActionType.FixedUpdate]?.Invoke();
        }

        public void ExitAction()
        {
            if (actionDictionary is null) return;
            actionDictionary[ActionType.Exit]?.Invoke();
        }
    }
    /// <summary>
    /// ステートの遷移情報
    /// </summary>
    [System.Serializable]
    public struct TransitionData
    {
        //遷移のトリガー
        [SerializeReference, SubclassSelector]
        public IStateTriggerable trigger;
        //正しい遷移先
        public ScriptableStateData trueState;
    }

    public enum ActionType
    {
        Enter,
        Update,
        FixedUpdate,
        Exit
    }
}