using UnityEngine;
using AsciiUtil.Tools;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

namespace AsciiUtil.UI
{
    public class StandardButton : AsciiButton
    {
        [SerializeField]
        private TweenAnimationPlayer feedbackPlayer;
        [SerializeField, Range(0, 1), Header("押されてからアクティブになるまでの時間 0で連打可")]
        private float pushIntervalSec;

        private bool canPush = true;
        public override async void OnPointerClick(PointerEventData eventData)
        {
            if (!canPush) return;
            canPush = false;
            feedbackPlayer?.PlayAnimation();
            gameEvent?.Raise();
            base.OnPointerClick(eventData);
            //再度押せるようになるのをインターバル分遅らせる
            if (pushIntervalSec == 0)
            {
                canPush = true;
                return;
            }
            await UniTask.Delay(System.TimeSpan.FromSeconds(pushIntervalSec));
            canPush = true;
        }
    }
}