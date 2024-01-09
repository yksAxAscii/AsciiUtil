using DG.Tweening;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Camera/ChangeCameraCollor")]
    public class ChangeCameraCollor : TweenAction
    {
        [SerializeField, Header("変更後の背景色")]
        private Color endValue;
        [SerializeField, Header("対象のカメラ(未選択だとメインカメラ")]
        private Camera targetCamera;

        public override Tween Play(Transform transform)
        {
            //ターゲットカメラがなければメインカメラを取得
            targetCamera = targetCamera != null ? targetCamera : Camera.main;

            //ShakeRotationを実行
            return targetCamera.DOColor(endValue, tweenParameter.Duration)
             .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}