using System;
using UniRx;

namespace AsciiUtil
{
    public class CharacterEvent
    {
        private string eventKey;
        public string EventKey => eventKey;
        private Subject<bool> eventSubject;
        public IObservable<bool> EventSubject => eventSubject;
        private IDisposable disposable;

        public CharacterEvent(string eventKey)
        {
            disposable = eventSubject = new Subject<bool>();
            this.eventKey = eventKey;
        }

        public void OnNext(bool value)
        {
            eventSubject?.OnNext(value);
        }

        public void OnCompleted()
        {
            eventSubject.OnCompleted();
        }

        public void Dispose()
        {
            OnCompleted();
            disposable.Dispose();
        }
    }
}
