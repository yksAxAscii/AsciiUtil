using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using Cysharp.Threading.Tasks;

namespace AsciiUtil
{
    public class HealthModel
    {
        private FloatReactiveProperty maxHealth;
        public ReadOnlyReactiveProperty<float> MaxHealth => maxHealth.ToReadOnlyReactiveProperty();
        private FloatReactiveProperty currentHealth;
        public ReadOnlyReactiveProperty<float> CurrentHealth => currentHealth.ToReadOnlyReactiveProperty();
        private UnityAction onDeath { get; set; }
        private CancellationTokenSource cts;
        protected const int deathHealthValue = 0;

        public HealthModel(float maxHealth, UnityAction onDeath = null)
        {
            this.maxHealth = new FloatReactiveProperty(maxHealth);
            currentHealth = new FloatReactiveProperty(maxHealth);
            this.onDeath += () =>
            {
                onDeath?.Invoke();
                cts.Cancel();
            };
            Initialize();
        }

        public async void Initialize()
        {
            cts = new CancellationTokenSource();
            try
            {
                await UniTask.WaitUntil(() => currentHealth.Value <= deathHealthValue, cancellationToken: cts.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            onDeath?.Invoke();
        }

        public virtual void Increasement(float value)
        {
            currentHealth.Value = Mathf.Clamp(currentHealth.Value + value, deathHealthValue, maxHealth.Value);
        }
        public virtual void Decreasement(float value)
        {
            currentHealth.Value = Mathf.Clamp(currentHealth.Value - value, deathHealthValue, maxHealth.Value);
        }

        public void Dispose()
        {
            cts.Cancel();
        }
    }
}