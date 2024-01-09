using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable]
    public class TweenParameter
    {
        [SerializeField]
        private float duration;
        public float Duration => duration;
        [SerializeField]
        private Ease ease = Ease.OutQuad;
        public Ease Ease => ease;
        [SerializeField]
        private bool isIgnoreTimeScale;
        public bool IsIgnoreTimeScale => isIgnoreTimeScale;
    }
}
