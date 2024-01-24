using UnityEngine;
using UnityEngine.Events;
using UniRx;
using DG.Tweening;

namespace AsciiUtil.UI
{
    public class AsciiPopupWindow : MonoBehaviour
    {
        [SerializeField]
        private FeedbackActionData openFeedback;
        [SerializeField]
        private FeedbackActionData closeFeedback;
        [SerializeField]
        private bool isOpenOnEnable;
        [SerializeField]
        private GameEvent onClosedEvent;
        private UnityAction onItitialized;
        public UnityAction OnItitialized => onItitialized;
        private UnityAction onOpened;
        public UnityAction OnOpened { get => onOpened; set => onOpened = value; }
        private UnityAction onClosed;
        public UnityAction OnClosed { get => onClosed; set => onClosed = value; }
        private const string OpenFeedbackKey = "Open";
        private const string CloseFeedbackKey = "Close";
        private void Awake()
        {
            onItitialized?.Invoke();
            onClosedEvent?.EventSubject.Subscribe(_ => ClosePopupWindow());
        }

        private void OnEnable()
        {
            if (!isOpenOnEnable) return;
            OpenPopupWindow();
        }

        public async void OpenPopupWindow()
        {
            await openFeedback.CreateSequence(transform).Play().AsyncWaitForCompletion();
            onOpened?.Invoke();
        }

        public async void ClosePopupWindow()
        {
            await closeFeedback.CreateSequence(transform).Play().AsyncWaitForCompletion();
            onClosed?.Invoke();
        }
    }
}
