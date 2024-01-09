using UnityEngine;
using DG.Tweening;
namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("Util/Material/SetFloat")]
    public class MaterialSetFloat : IStateActionable
    {
        [SerializeField, Header("変更したいパラメータ名")]
        private string paramName;
        [SerializeField, Header("変更後の値")]
        private float toValue;
        [SerializeField, Header("Tweenのパラメータ")]
        private TweenParameter tweenParameter;
        [SerializeField, Header("相対的な変化かどうか")]
        private bool isRelative;
        [SerializeField, Header("SharedMaterialを変更するかどうか")]
        private bool isSharedMaterial;
        private Material material;

        public void Action(AsciiStateMachine stateMachine)
        {
            //マテリアルがnullならstateMachineのマテリアルを取得
            //isSharedMaterialがtrueならsharedMaterialを、falseならmaterialを使用する
            material = material != null ? material : isSharedMaterial == true ? stateMachine.GetComponent<Renderer>().sharedMaterial : stateMachine.GetComponent<Renderer>().material;
            
            material.DOFloat(toValue, paramName, tweenParameter.Duration)
            .SetEase(tweenParameter.Ease)
            .SetRelative(isRelative)
            .SetUpdate(tweenParameter.IsIgnoreTimeScale);
        }

    }
}