using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace AsciiUtil
{
    public class TweenSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TweenParameter tweenParameter;
        private float tweenValue;

        private void Start()
        {
            this.ObserveEveryValueChanged(x => tweenValue)
            .Subscribe(x => slider.value = x)
            .AddTo(gameObject);
        }
        public void UpdateValue(float value)
        {
            DOTween.To(
                () => tweenValue,
                x => tweenValue = x,
                value,
                tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetLink(gameObject);
        }
    }
}
