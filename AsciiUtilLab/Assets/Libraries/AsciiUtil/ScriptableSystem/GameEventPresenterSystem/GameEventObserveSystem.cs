using UnityEngine;
using AsciiUtil.GameEvents;
using System;
using UniRx;

namespace AsciiUtil
{
    [CreateAssetMenu(menuName = "ScriptableSystem/GameEventObserveSystem")]
    public class GameEventObserveSystem : ScriptableObject, IScriptableSystemable
    {
        [SerializeField]
        private GameEvent[] gameEvents;
        private GameEvent gameEventCache;

        public IObservable<Unit> GetGameEventObservable(string targetKey)
        {
            return FindGameEvent(targetKey).EventSubject;
        }

        public void RaiseAnyGameEvent(string targetKey)
        {
            gameEventCache = FindGameEvent(targetKey);
            gameEventCache?.Raise();
        }

        public void Dispose(string targetKey)
        {
            gameEventCache = FindGameEvent(targetKey);
            gameEventCache?.Dispose();
        }

        private GameEvent FindGameEvent(string targetKey)
        {
            foreach (var gameEvent in gameEvents)
            {
                if (gameEvent.EventKey != targetKey) continue;
                return gameEvent;
            }
            Debug.LogError($"{targetKey} のゲームイベントが見つからないよ");
            return null;
        }
    }
}
