using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace AsciiUtil
{
    public class TweenAnimationPlayer : MonoBehaviour
    {
        [SerializeField, Header("フィードバックアクションデータ")]
        private FeedbackActionData[] feedbackData;
        private Dictionary<string, Tween> feedbackActions = new Dictionary<string, Tween>();

        private void Awake()
        {
            CreateSequence();
        }

        private void OnEnable()
        {
            RewindAllActions();
        }

        /// <summary>
        /// Uncomplete時に実行するアクションを設定
        /// </summary>
        /// <param name="feedbackKey"></param>
        /// <param name="action"></param> <summary>
        public void SetOnComplete(string feedbackKey, UnityAction action)
        {
            feedbackActions[feedbackKey].OnComplete(() => action.Invoke());
        }

        /// <summary>
        /// TweenAnimationを再生
        /// </summary>
        /// <param name="feedbackKey"></param>
        /// <returns></returns>
        public Tween PlayAnimation(string feedbackKey = null)
        {
            Sequence value = DOTween.Sequence();
            //キーの指定なければ全て再生(フィードバック1個しか登録してないときとかに使う気がする)
            if (feedbackKey is null)
            {
                foreach (var action in feedbackActions)
                {
                    value.Append(action.Value).Restart();
                }
                return value;
            }
            //キーの指定があれば指定キーのフィードバック再生
            value.Append(feedbackActions[feedbackKey]).Restart();
            return value;
        }

        /// <summary>
        /// 全てのアクションを巻き戻し
        /// </summary>
        private void RewindAllActions()
        {
            foreach (var action in feedbackActions)
            {
                action.Value.Restart();
            }
        }

        private void CreateSequence()
        {
            //フィードバックデータからフィードバックアクションを生成
            foreach (var feedback in feedbackData)
            {
                (string key, Tween sequence) tween = feedback.CreateSequence(transform);
                //再生終了時に巻き戻し
                tween.sequence.OnComplete(() =>
             {
                 tween.sequence.Restart();
                 tween.sequence.Pause();
             })
                .Pause();
                feedbackActions.Add(tween.key, tween.sequence);
            }
        }

    }
}
