using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Rotate/ShakeRotate")]
    public class ShakeRotate : TweenAction
    {
        [SerializeField, Header("振動の強さ")]
        private float strength;
        [SerializeField, Header("振動の回数")]
        private int shakeNum;
        public override Tween Play(Transform transform)
        {
            return transform.DOShakeRotation(strength, tweenParameter.Duration, shakeNum)
            .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}