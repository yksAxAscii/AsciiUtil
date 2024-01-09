using UnityEngine;
using TMPro;
using DG.Tweening;
using AsciiUtil.Tools;
using UniRx;

namespace AsciiUtil.GameCoreSystem.Health
{
    public class HealthTextView : HealthView
    {
        [SerializeField, Header("変更したいテキスト")]
        private TextMeshProUGUI text;

        public override void Initialize(float maxHealth)
        {
            base.Initialize(maxHealth);
            text.text = maxHealth.ToString();
            this.ObserveEveryValueChanged(_ => currentHealth)
            .Subscribe(_ => text.text = currentHealth.ToString());
        }
    }
}