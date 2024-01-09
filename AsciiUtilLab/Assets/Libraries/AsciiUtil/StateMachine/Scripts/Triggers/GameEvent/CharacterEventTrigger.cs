using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("GameEvent/CharacterEventTrigger")]
    public class CharacterEventTrigger : IStateTriggerable
    {
        [SerializeField]
        private string eventKey;
        [System.NonSerialized]
        private CharacterEventPresenter characterEventPresenter;
        [System.NonSerialized]
        private bool value;

        public void Initialize(AsciiStateMachine stateMachine)
        {
            value = false;
            if (characterEventPresenter is null)
            {
                characterEventPresenter = stateMachine.GetComponent<CharacterEventPresenter>();
            }
            characterEventPresenter.Initialize();
            characterEventPresenter.Subscribe(eventKey, () => value = true);
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return value;
        }

    }
}