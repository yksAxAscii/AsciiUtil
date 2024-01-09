using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Transform/Rotate/RotateTo")]
    public class RotateTo : TweenAction
    {
        [SerializeField, ClampVector(0, 360), Header("回転方向")]
        private Vector3 rotateDirection;
        public override Tween Play(Transform transform)
        {
            return transform.DORotate(rotateDirection, tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}