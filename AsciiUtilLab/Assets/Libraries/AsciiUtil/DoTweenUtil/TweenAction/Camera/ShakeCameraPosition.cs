using DG.Tweening;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Camera/ShakeCameraPosition")]
    public class ShakeCameraPosition : TweenAction
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

            //ShakePositionを実行
            return targetCamera.DOShakePosition(strength, tweenParameter.Duration, shakeNum)
            .SetEase(tweenParameter.Ease)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }
    }
}