using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;

public class AsyncTimer
{
    private CancellationTokenSource cancellationTokenSource;// = new CancellationTokenSource();
    private float limitTimeSec;
    private UnityAction timeUpAction;
    private bool isTimerStart;
    //TODO 進捗率を返す

    public AsyncTimer(float limitTimeSec, UnityAction timeUpAction, bool isStart = false)
    {
        this.limitTimeSec = limitTimeSec;
        this.timeUpAction = timeUpAction;
        cancellationTokenSource = new CancellationTokenSource();
        if (isStart)
        {
            TimerStart();
        }
    }

    public async void TimerStart()
    {
        if (isTimerStart) return;
        isTimerStart = true;
        await UniTask.Delay(System.TimeSpan.FromSeconds(limitTimeSec), false, PlayerLoopTiming.Update);
        isTimerStart = false;
        timeUpAction?.Invoke();
    }

    //TODO キャンセル処理
    public void TimerReset()
    {
        //cancellationTokenSource.Cancel();
    }
}