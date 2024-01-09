using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using UniRx;
using DG.Tweening;

namespace AsciiUtil.UI
{
    public class AsciiPopupWindow : MonoBehaviour
    {
        [SerializeField]
        private TweenAnimationPlayer actionableContents;
        [SerializeField]
        private GameEvent onClosedEvent;
        private UnityAction onItitialized;
        public UnityAction OnItitialized => onItitialized;
        private UnityAction onOpened;
        public UnityAction OnOpened => onOpened;
        private UnityAction onClosed;
        public UnityAction OnClosed => onClosed;
        private void Start()
        {
            onItitialized?.Invoke();
            onClosed += () =>
            {
                gameObject.SetActive(false);
            };
            onClosedEvent?.EventSubject.Subscribe(_ => ClosePopupWindow());
        }

        public async void OpenPopupWindow()
        {
            actionableContents.gameObject.SetActive(true);
            await actionableContents.PlayAnimation("Open").AsyncWaitForCompletion();
            onOpened?.Invoke();
        }

        public async void ClosePopupWindow()
        {
            await actionableContents.PlayAnimation("Close").AsyncWaitForCompletion();
            onClosed?.Invoke();
        }
    }
}
