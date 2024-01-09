using UnityEngine;
using UniRx;
namespace AsciiUtil.GameEvents
{
    /// <summary>
    /// 1回の呼び出しでDisposeされるゲームイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AsciiUtil/GameEvent/OneShotGameEvent")]
    public class OneShotGameEvent : GameEvent
    {
    }
}