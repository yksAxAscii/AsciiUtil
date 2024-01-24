using UnityEngine;
using UnityEngine.Events;
using UniRx;
using DG.Tweening;
namespace AsciiUtil.UI
{
    public class SlideWindow : MonoBehaviour
    {
        [SerializeField]
        private FeedbackActionData openFeedback;
        [SerializeField]
        private FeedbackActionData closeFeedback;
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
            //初期化があれば実行
            onItitialized?.Invoke();
            //閉じるアニメーションが終了したら非アクティブにする
            onClosed += () =>
            {
                gameObject.SetActive(false);
            };
            //閉じるイベントが発火したら閉じる
            onClosedEvent?.EventSubject.Subscribe(_ => CloseWindow());
        }

        private void OnEnable()
        {
            OpenWindow();
        }

        /// <summary>
        /// ウィンドウを開きます
        /// </summary>
        public async void OpenWindow()
        {
            await openFeedback.CreateSequence(transform).Play().AsyncWaitForCompletion();
            onOpened?.Invoke();
        }

        /// <summary>
        /// ウィンドウを閉じます
        /// </summary>
        public async void CloseWindow()
        {
            await closeFeedback.CreateSequence(transform).Play().AsyncWaitForCompletion();
            onClosed?.Invoke();
        }
    }
}
