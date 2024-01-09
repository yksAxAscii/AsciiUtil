using UnityEngine;
using UnityEngine.Events;
using UniRx;
using AsciiUtil.Tools;

namespace AsciiUtil
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;
        public float MaxHealth => maxHealth;
        [SerializeField]
        private TweenAnimationPlayer feedbackPlayer;
        [SerializeField]
        private HealthView view;
        private HealthModel model;
        public UnityAction onDeath { get; set; }

        public void Initialize(float maxHealth, UnityAction onDeath = null)
        {
            model = new HealthModel(maxHealth,onDeath);
            this.maxHealth = maxHealth;
            view.Initialize(maxHealth);
            //モデルのヘルスに変化があったらViewをアップデート
            model.CurrentHealth.Subscribe(value => ViewUpdate(value))
            .AddTo(gameObject);
        }

        private void ViewUpdate(float health)
        {
            view.HealthViewUpdate(health);
        }

        public void TakeDamage(float value)
        {
            feedbackPlayer?.PlayAnimation();
            model.Decreasement(value);
        }

        private void OnDisable()
        {
            model.Dispose();
        }
    }
}