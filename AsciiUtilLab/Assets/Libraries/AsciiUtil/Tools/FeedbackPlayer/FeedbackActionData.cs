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

        public Tween CreateSequence(Transform transform)
        {
            var tween = DOTween.Sequence();
            foreach (var feedback in feedbackActions)
            {
                var feedbackTween = feedback?.Play(transform);
                if (feedbackTween == null) continue;
                tween.Append(feedback.Play(transform));
            }
            return tween;
        }
    }
}
