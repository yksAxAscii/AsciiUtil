using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Move/MoveTo")]
    public class MoveTo : TweenAction
    {
        [SerializeField, Header("移動先")]
        private Vector3 moveTo;
        public override Tween Play(Transform transform)
        {
            return transform.DOMove(moveTo, tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}