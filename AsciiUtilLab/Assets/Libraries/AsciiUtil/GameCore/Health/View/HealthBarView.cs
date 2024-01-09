using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace AsciiUtil.GameCoreSystem.Health
{
    public class HealthBarView : HealthView
    {
        [SerializeField]
        private Image image;

        public override void Initialize(float maxHealth)
        {
            base.Initialize(maxHealth);
            image.fillAmount = 1;
            this.ObserveEveryValueChanged(_ => currentHealth)
            .Subscribe(_ => image.fillAmount = (currentHealth / maxHealth));
        }
    }
}