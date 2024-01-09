using UnityEngine;
using DG.Tweening;

namespace AsciiUtil
{
    /// <summary>
    /// フィードバックアクションのテンプレートデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AsciiUtil/FeedbackActionData")]
    public class FeedbackActionData : ScriptableObject
    {
        [SerializeField, Header("フィードバックのキー")]
        private string feedbackKey;
        [SerializeReference, SubclassSelector, Header("フィードバックアクション")]
        private ITweenActionable[] feedbackActions;

        public (string key, Tween sequence) CreateSequence(Transform transform)
        {
            var tweenCache = DOTween.Sequence();
            foreach (var feedback in feedbackActions)
            {
                var tween = feedback?.Play(transform);
                if (tween == null) continue;
                tweenCache.Append(feedback.Play(transform));
            }
            return (feedbackKey, tweenCache);
        }
    }
}
