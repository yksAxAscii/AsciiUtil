using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace AsciiUtil.Tools
{
    public class InputActionHundler : MonoBehaviour
    {
        //マウス座標
        [SerializeField]
        private Vector2ReactiveProperty mousePosition = new Vector2ReactiveProperty();
        public IReadOnlyReactiveProperty<Vector2> MousePosition => mousePosition;
        //タッチ(クリック)座標
        [SerializeField]
        private Vector2ReactiveProperty touchPosition = new Vector2ReactiveProperty();
        public IReadOnlyReactiveProperty<Vector2> TouchPosition => touchPosition;
        //リリース座標
        [SerializeField]
        private Vector2ReactiveProperty releasePosition = new Vector2ReactiveProperty();
        public IReadOnlyReactiveProperty<Vector2> ReleasePosition => releasePosition;
        //タッチ(クリック)
        [SerializeField]
        private BoolReactiveProperty isTouch = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<bool> IsTouch => isTouch;
        //リリース
        [SerializeField]
        private BoolReactiveProperty isRelease = new BoolReactiveProperty();
        public IReadOnlyReactiveProperty<bool> IsRelease => isRelease;
        //フリック
        [SerializeField]
        private float flickLimitTimeSec;
        [SerializeField]
        private float flickThreshold;
        [SerializeField]
        private ReactiveProperty<FlickDirection> flickDir = new ReactiveProperty<FlickDirection>();
        public IReadOnlyReactiveProperty<FlickDirection> FlickDir => flickDir;
        //スワイプ
        [SerializeField]
        private Vector2ReactiveProperty swipeDistance = new Vector2ReactiveProperty();
        public IReadOnlyReactiveProperty<Vector2> SwipeDistance => swipeDistance;

        void Start()
        {
            //マウス座標の更新
            this.UpdateAsObservable()
                .Subscribe(_ => mousePosition.Value = Input.mousePosition);
            //タッチ(クリック)時処理
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    isTouch.Value = true;
                    isRelease.Value = false;
                    touchPosition.Value = Input.mousePosition;
                });
            //リリース時処理
            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Subscribe(_ =>
                {
                    isTouch.Value = false;
                    isRelease.Value = true;
                    releasePosition.Value = Input.mousePosition;
                });
            //フリック判定,処理
            var start = this.UpdateAsObservable()
                 .Where(_ => Input.GetMouseButtonDown(0))
                 .Select(_ => Input.mousePosition);
            var end = this.UpdateAsObservable()
                .TakeUntil(Observable.Timer(System.TimeSpan.FromSeconds(flickLimitTimeSec)))
                .Where(_ => Input.GetMouseButtonUp(0))
                .Select(_ => Input.mousePosition)
                .Take(1);
            start.SelectMany(startPos => end.Select(endPos => startPos - endPos))
                .Subscribe(distance =>
                {
                    flickDir.Value = FlickDirection.NONE;
                    if (distance.sqrMagnitude < Mathf.Pow(flickThreshold, 2)) return;
                    flickDir.Value = GetFlickDirection(distance);
                });
        }

        /// <summary>
        /// フリック方向取得
        /// </summary>
        private FlickDirection GetFlickDirection(Vector2 direction)
        {
            var flickDirection = FlickDirection.NONE;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    flickDirection = FlickDirection.LEFT;
                }
                else
                {
                    flickDirection = FlickDirection.RIGHT;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    flickDirection = FlickDirection.DOWN;
                }
                else
                {
                    flickDirection = FlickDirection.UP;
                }
            }
            return flickDirection;
        }

        private Vector2 CalculateSwipeDistance()
        {
            return releasePosition.Value - touchPosition.Value;
        }
    }
}

public enum FlickDirection
{
    NONE,
    RIGHT,
    LEFT,
    UP,
    DOWN,
}