using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
namespace AsciiUtil.UI
{
    [AddComponentMenu("AsciiUtil/UI/Buttons/AsciiRadioButton")]
    public class AsciiRadioButtonGroup : AsciiRadioButtonGroup<Unit>
    {
        public override void AddConfirmAction(string key, UnityAction<Unit> action)
        {
            base.AddConfirmAction(key, action);
        }

        public override void Confirm(Unit value = default(Unit))
        {
            base.Confirm(Unit.Default);
        }

        public override void AddClickAction(string key, UnityAction action)
        {
            base.AddClickAction(key, action);
        }
    }

    public class AsciiRadioButtonGroup<T> : MonoBehaviour
    {
        [SerializeField]
        private RadioButtonInfo<T>[] buttonInfos;
        public RadioButtonInfo<T>[] ButtonInfos => buttonInfos;
        private AsciiButton currentSelectionButton;

        /// <summary>
        /// 確定時の処理を追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public virtual void AddConfirmAction(string key, UnityAction<T> action)
        {
            GetButtonInfo(key).onSelectionConfirm += action;
        }

        /// <summary>
        /// ラジオボタン押下時の処理を追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public virtual void AddClickAction(string key, UnityAction action)
        {
            GetButtonInfo(key).button.ButtonActions.OnButtonClick += action;
        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="value"></param>
        public virtual void Confirm(T value)
        {
            if (currentSelectionButton == null) return;
            OnSelectionConfirmAction(currentSelectionButton)?.Invoke(value);
        }

        private void Start()
        {
            //各種ボタン押下時処理
            foreach (var buttonInfo in buttonInfos)
            {
                buttonInfo.button.ButtonActions.OnButtonClick += () => OnClick(buttonInfo.button);
            }
        }

        /// <summary>
        /// クリック時処理
        /// </summary>
        /// <param name="button"></param>
        private void OnClick(AsciiButton button)
        {
            currentSelectionButton = button;
        }

        private UnityAction<T> OnSelectionConfirmAction(AsciiButton selectionButton)
        {
            return buttonInfos.FirstOrDefault(X => X.button == selectionButton).onSelectionConfirm;
        }

        private RadioButtonInfo<T> GetButtonInfo(string key)
        {
            var value = buttonInfos.FirstOrDefault(X => X.key == key);
            if (value == null)
            {
                AsciiDebug.LogError($"key:{key}のボタンが見つかりません");
            }
            return value;
        }
    }

    [System.Serializable]
    public class RadioButtonInfo<T>
    {
        public string key;
        public AsciiButton button;
        public UnityAction<T> onSelectionConfirm;
    }
}