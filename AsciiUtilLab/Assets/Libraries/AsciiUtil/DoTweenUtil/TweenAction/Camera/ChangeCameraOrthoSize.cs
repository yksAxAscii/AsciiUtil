using DG.Tweening;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Camera/ChangeCameraOrthoSize")]
    public class ChangeCameraOrthoSize : TweenAction
    {
        [SerializeField, Header("変更後のOrthoSize")]
        private float endValue;
        [SerializeField, Header("対象のカメラ(未選択だとメインカメラ)")]
        private Camera targetCamera;

        public override Tween Play(Transform transform)
        {
            //ターゲットカメラがなければメインカメラを取得
            targetCamera = targetCamera != null ? targetCamera : Camera.main;

            //ShakeRotationを実行
            return targetCamera.DOOrthoSize(endValue, tweenParameter.Duration)
             .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}