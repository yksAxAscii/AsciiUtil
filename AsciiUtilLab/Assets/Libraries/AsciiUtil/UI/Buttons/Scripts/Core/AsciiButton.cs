using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

namespace AsciiUtil.UI
{
    [System.Serializable]
    public class ButtonFeedbackAnimations
    {
        [SerializeReference, SubclassSelector, Header("ボタンにカーソルが乗った時のアニメーション")]
        public ITweenActionable OnButtonEnter;
        [SerializeReference, SubclassSelector, Header("ボタンからカーソルが離れた時のアニメーション")]
        public ITweenActionable OnButtonExit;
        [SerializeReference, SubclassSelector, Header("ボタンを押した時のアニメーション")]
        public ITweenActionable OnButtonDown;
        [SerializeReference, SubclassSelector, Header("ボタンを離した時のアニメーション")]
        public ITweenActionable OnButtonUp;
        [SerializeReference, SubclassSelector, Header("ボタンをクリックした時のアニメーション")]
        public ITweenActionable OnButtonClick;
    }
    public class ButtonAction
    {
        public UnityAction OnButtonEnter;
        public UnityAction OnButtonExit;
        public UnityAction OnButtonDown;
        public UnityAction OnButtonUp;
        public UnityAction OnButtonClick;
    }
    public class AsciiButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Header("フィードバックアニメーション")]
        private ButtonFeedbackAnimations uiFeedbackAnimations;
        public ButtonFeedbackAnimations UIFeedbackAnimations => uiFeedbackAnimations;
        private ButtonAction buttonActions = new ButtonAction();
        public ButtonAction ButtonActions => buttonActions;
        [SerializeField, Header("ボタン押下時のイベント")]
        protected GameEvent gameEvent;
        [SerializeField]
        protected bool isInteractable = true;
        public bool IsInteractable { set => isInteractable = value; }
        [SerializeField, Header("アニメーション後にイベントを発行するか")]
        private bool isActionAfterAnimation;

        private Tween currentAnimationTween;

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (isActionAfterAnimation)
            {
                //isActionAfterAnimationが有効の時はOnCompleteでアクションを呼び出す
                uiFeedbackAnimations.OnButtonDown?.Play(transform).OnComplete(() => buttonActions.OnButtonDown?.Invoke());
                return;
            }
            uiFeedbackAnimations.OnButtonDown?.Play(transform);
            buttonActions.OnButtonDown?.Invoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (isActionAfterAnimation)
            {
                //isActionAfterAnimationが有効の時はOnCompleteでアクションを呼び出す
                uiFeedbackAnimations.OnButtonUp?.Play(transform).OnComplete(() => buttonActions.OnButtonUp?.Invoke());
                return;
            }
            uiFeedbackAnimations.OnButtonUp?.Play(transform);
            buttonActions.OnButtonUp?.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            if (currentAnimationTween != null)
            {
                if (isActionAfterAnimation)
                {
                    //isActionAfterAnimationが有効の時はOnCompleteでアクションを呼び出す
                    uiFeedbackAnimations.OnButtonClick?.Play(transform).OnComplete(() => buttonActions.OnButtonClick?.Invoke());
                    return;
                }

                if (!currentAnimationTween.IsPlaying())
                {
                    currentAnimationTween = uiFeedbackAnimations.OnButtonClick?.Play(transform);
                }
            }
            buttonActions.OnButtonClick?.Invoke();
            gameEvent?.Raise();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            uiFeedbackAnimations.OnButtonEnter?.Play(transform);
            buttonActions.OnButtonEnter?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            uiFeedbackAnimations.OnButtonExit?.Play(transform);
            buttonActions.OnButtonExit?.Invoke();
        }

    }
}