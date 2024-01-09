using DG.Tweening;
using UnityEngine;

namespace AsciiUtil
{
    public class TweenAction : ITweenActionable
    {
        [SerializeField]
        protected TweenParameter tweenParameter;

        public virtual Tween Play(Transform transform)
        {
            return DOTween.Sequence();
        }
    }
}
