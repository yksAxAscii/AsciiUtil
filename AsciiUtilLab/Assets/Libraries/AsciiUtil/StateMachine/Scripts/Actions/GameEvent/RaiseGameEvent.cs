using UnityEngine;
namespace AsciiUtil.StateMachine
{
    [System.Serializable, AddTypeMenu("Util/GameEvents/Raise")]
    public class RaiseGameEvent : IStateActionable
    {
        [SerializeField]
        private GameEvent gameEvent;

        public void Action(AsciiStateMachine stateMachine)
        {
            gameEvent?.Raise();
        }

    }
}