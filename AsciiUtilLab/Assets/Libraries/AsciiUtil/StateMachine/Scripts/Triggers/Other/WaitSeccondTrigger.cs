using UnityEngine;
using System.Threading;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

namespace AsciiUtil.StateMachine
{
    /// <summary>
    /// 指定秒数待機後にフラグが立つトリガー
    /// </summary>
    [System.Serializable, AddTypeMenu("Other/WaitSeccondTrigger")]
    public class WaitSeccondTrigger : IStateTriggerable
    {
        [SerializeField]
        private float waitSeccond;
        [SerializeField]
        private bool isIgnoreTimeScale;

        [System.NonSerialized]
        private bool value;
        [System.NonSerialized]
        private CancellationTokenSource cancellationTokenSource;
        public async void Initialize(AsciiStateMachine stateMachine)
        {
            //非アクティブになったらタスクをキャンセル
            cancellationTokenSource = new CancellationTokenSource();
            stateMachine.OnDisableAsObservable().Subscribe(_ => cancellationTokenSource?.Cancel())
            .AddTo(stateMachine.gameObject);

            value = false;

            bool delayCanceled = await UniTask.Delay(System.TimeSpan.FromSeconds(waitSeccond), isIgnoreTimeScale, PlayerLoopTiming.Update, cancellationTokenSource.Token).SuppressCancellationThrow();

            //キャンセルされたらトークンをDispose
            if (delayCanceled)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                return;
            }
            value = true;
        }

        public bool Verify(AsciiStateMachine stateMachine)
        {
            return value;
        }
    }
}