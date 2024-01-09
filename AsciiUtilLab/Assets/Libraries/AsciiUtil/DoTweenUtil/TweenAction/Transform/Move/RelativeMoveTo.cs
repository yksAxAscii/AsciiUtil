using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Move/RelativeMoveTo")]
    public class RelativeMoveTo : TweenAction
    {
        [SerializeField, Header("移動先")]
        private Vector3 moveTo;
        public override Tween Play(Transform transform)
        {
            return transform.DOMove(moveTo, tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetRelative(true)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}