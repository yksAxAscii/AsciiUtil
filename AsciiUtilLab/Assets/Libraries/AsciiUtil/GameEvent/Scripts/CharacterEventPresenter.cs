using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace AsciiUtil
{
    public class CharacterEventPresenter : MonoBehaviour
    {
        [System.Serializable]
        private class CharacterEventParameter
        {
            public string eventKey;
            private CharacterEvent eventCache;
            public CharacterEvent CreateEvent()
            {
                if (eventCache is null)
                {
                    eventCache = new CharacterEvent(eventKey);
                }
                return eventCache;
            }
        }

        [SerializeField]
        private CharacterEventParameter[] characterEventParameters;
        private List<CharacterEvent> events = new List<CharacterEvent>();
        private CharacterEvent eventCache;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            events.Clear();
            foreach (var parameter in characterEventParameters)
            {
                events.Add(parameter.CreateEvent());
            }
        }

        private CharacterEvent FindEvents(string eventKey)
        {
            eventCache = null;
            foreach (var characterEvent in events)
            {
                if (characterEvent.EventKey != eventKey) continue;
                return characterEvent;
            }
            Debug.LogError($"{eventKey} のイベントが見つからないよ");
            return null;
        }

        public void Raise(string eventKey)
        {
            FindEvents(eventKey).OnNext(true);
        }

        public void Subscribe(string eventKey, UnityAction action)
        {
            FindEvents(eventKey).EventSubject.Subscribe(_ => action.Invoke()).AddTo(gameObject);
        }

        private void OnDisable()
        {
            events.Clear();
        }
    }




}
