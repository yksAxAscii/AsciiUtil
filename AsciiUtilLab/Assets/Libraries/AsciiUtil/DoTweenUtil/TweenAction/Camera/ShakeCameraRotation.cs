using DG.Tweening;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Camera/ShakeCameraRotation")]
    public class ShakeCameraRotation : TweenAction
    {
        [SerializeField, Header("振動の強さ")]
        private float strength;
        [SerializeField, Header("振動の回数")]
        private int shakeNum;
        [SerializeField, Header("振動させるカメラ")]
        private Camera targetCamera;

        public override Tween Play(Transform transform)
        {
            //ターゲットカメラがなければメインカメラを取得
            targetCamera = targetCamera != null ? targetCamera : Camera.main;
            //ShakeRotationを実行
            return targetCamera.DOShakeRotation(strength, tweenParameter.Duration, shakeNum)
             .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}