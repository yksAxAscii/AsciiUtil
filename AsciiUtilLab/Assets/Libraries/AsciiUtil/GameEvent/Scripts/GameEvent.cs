using UnityEngine;
using System;
using UniRx;
namespace AsciiUtil
{
    /// <summary>
    /// 複数回呼べるゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "GameEvent/ReusableGameEvent")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField]
        protected string eventKey;
        public string EventKey => eventKey;
        [SerializeReference, SubclassSelector]
        private IGameEventActionable[] onRaisedAction;
        [System.NonSerialized]
        protected Subject<Unit> eventSubject = new Subject<Unit>();
        public IObservable<Unit> EventSubject => eventSubject;
        [System.NonSerialized]
        private bool isInitialized = false;

        public virtual void Raise()
        {
            if (!isInitialized)
            {
                eventSubject.Subscribe(_ =>
                {
                    if (onRaisedAction != null)
                    {
                        foreach (var action in onRaisedAction)
                        {
                            action.Action();
                        }
                    }
                });
                isInitialized = true;
            }
            eventSubject.OnNext(Unit.Default);
        }

        public virtual void Dispose()
        {
            eventSubject.Dispose();
        }
    }
}