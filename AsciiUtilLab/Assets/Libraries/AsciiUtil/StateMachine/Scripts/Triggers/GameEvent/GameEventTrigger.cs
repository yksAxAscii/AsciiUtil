using UnityEngine;
using UniRx;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("GameEvent/GameEventTrigger")]
    public class GameEventTrigger : IStateTriggerable
    {
        [SerializeField]
        private GameEvent targetEvent;
        [System.NonSerialized]
        private bool value;

        public void Initialize(AsciiStateMachine stateMachine)
        {
            value = false;
            targetEvent.EventSubject.Subscribe(_ => value = true);
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return value;
        }

    }
}