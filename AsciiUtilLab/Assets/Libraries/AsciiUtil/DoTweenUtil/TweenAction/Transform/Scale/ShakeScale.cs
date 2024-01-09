using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Scale/ShakeScale")]
    public class ShakeScale : TweenAction
    {
        [SerializeField]
        private float strength;
        [SerializeField]
        private int shakeNum;
        public override Tween Play(Transform transform)
        {
            return transform.DOShakeScale(strength, tweenParameter.Duration, shakeNum)
            .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }

    }
}
