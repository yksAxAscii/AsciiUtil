using UnityEngine;
using System;
using UniRx;
namespace AsciiUtil
{
    /// <summary>
    /// 複数回呼べるゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AsciiUtil/GameEvent/ReusableGameEvent")]
    public class GameEvent : GameEvent<Unit>
    {
        public override void Raise(Unit value = default(Unit))
        {
            eventSubject.OnNext(Unit.Default);
        }
    }
    public class GameEvent<T> : ScriptableObject
    {
        [SerializeField]
        protected string eventKey;
        public string EventKey => eventKey;
        [System.NonSerialized]
        protected Subject<T> eventSubject = new Subject<T>();
        public IObservable<T> EventSubject => eventSubject;

        public virtual void Raise(T value)
        {
            eventSubject.OnNext(value);
        }

        public virtual void Dispose()
        {
            eventSubject.Dispose();
        }
    }
}