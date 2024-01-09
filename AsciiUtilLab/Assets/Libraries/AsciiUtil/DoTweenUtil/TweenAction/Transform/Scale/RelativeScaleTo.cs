using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Scale/RelativeScaleTo")]
    public class RelativeScaleTo : TweenAction
    {
        [SerializeField, Header("変更後のサイズ")]
        private Vector3 scaleTo;

        public override Tween Play(Transform transform)
        {
            return transform.DOScale(scaleTo, tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetRelative(true)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }

    }
}
