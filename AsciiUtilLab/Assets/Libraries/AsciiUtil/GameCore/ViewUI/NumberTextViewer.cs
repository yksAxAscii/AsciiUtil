using TMPro;
using UnityEngine;
using DG.Tweening;
using AsciiUtil.Tools;
using UniRx;

namespace AsciiUtil.UI
{
    /// <summary>
    /// 数字をいい感じに表示してくれるテキストビュー
    /// </summary>
    public class NumberTextViewer : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI text;
        [SerializeField, Header("Tweenパラメータ")]
        private TweenParameter tweenParameter;
        [SerializeField]
        private FeedbackActionData feedback;
        [SerializeField, Header("表示する小数点以下の桁数")]
        private int fractionDigits = 0;
        public int FractionDigits { get => fractionDigits; set => fractionDigits = value; }
        [SerializeField]
        private bool isRoundUp;
        [SerializeField]
        private bool isConvertUnit;
        private float currentValue;
        private float previousValue;
        private Tween currentTween;

        private void Start()
        {
            this.ObserveEveryValueChanged(_ => currentValue)
            .Subscribe(x =>
            {
                if (isConvertUnit)
                {
                    text.text = ConvertNumberToUnit(x);
                    return;
                }
                if (isRoundUp)
                {
                    text.text = Mathf.CeilToInt(x).ToString();
                    return;
                }
                text.text = x.ToString($"F{fractionDigits}");
            })
            .AddTo(gameObject);
        }

        public void UpdateTextValue(float value)
        {
            currentTween.Kill();
            currentTween = DOTween.To(
                 () => previousValue,
                 x => currentValue = x,
                 value,
                 tweenParameter.Duration)
                 .SetEase(tweenParameter.Ease)
                 .SetUpdate(true)
                 .OnKill(() => previousValue = value)
                 .OnComplete(() => previousValue = currentValue);
            feedback?.CreateSequence(transform).Play();
        }

        private string ConvertNumberToUnit(float number)
        {
            const float b = 1000000000;
            const float m = 1000000;
            const float k = 1000;

            if (number >= b)
            {
                return (number / b).ToString($"F{fractionDigits}") + "B";
            }
            else if (number >= m)
            {
                return (number / m).ToString($"F{fractionDigits}") + "M";
            }
            else if (number >= k)
            {
                return (number / k).ToString($"F{fractionDigits}") + "K";
            }
            else
            {
                if (isRoundUp)
                {
                    return Mathf.CeilToInt(number).ToString();
                }
                return number.ToString();
            }
        }
    }
}