using UnityEngine;
using UniRx;
namespace AsciiUtil.GameEvents
{
    /// <summary>
    /// 1回の呼び出しでDisposeされるゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "GameEvent/OneShotGameEvent")]
    public class OneShotGameEvent : GameEvent
    {
        public override void Raise()
        {
            eventSubject.OnNext(Unit.Default);
            eventSubject.OnCompleted();
            Dispose();
        }
    }
}