using UnityEngine;
using DG.Tweening;

namespace AsciiUtil
{
    public abstract class HealthView : MonoBehaviour
    {
        [SerializeField, Header("フィードバック\nなくてもOK")]
        protected FeedbackActionData feedback;
        [SerializeField, Header("増減にかかる時間")]
        protected float durationSec;
        [SerializeField, Header("増減の計算方法")]
        protected Ease ease;
        protected Tween currentTween;
        protected float maxHealth;
        protected float currentHealth;
        protected float previousHealth;
        public virtual void Initialize(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            previousHealth = currentHealth;
            if (!currentTween.IsActive()) return;
            currentTween.Kill();
        }
        public virtual void HealthViewUpdate(float health)
        {
            if (currentTween.IsActive())
            {
                currentTween.Kill();
            }
            previousHealth = currentHealth;
            feedback?.CreateSequence(transform).Play();
            currentTween = DOTween.To(
                () => currentHealth,
                value => currentHealth = value,
                health,
                durationSec)
                .SetEase(ease);
        }
    }
}