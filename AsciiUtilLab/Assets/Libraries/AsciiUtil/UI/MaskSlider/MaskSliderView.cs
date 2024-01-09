using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// マスクを使ったスライダーの描画
/// </summary>
public class MaskSliderView : MonoBehaviour
{
    [SerializeField]
    private Image backGroundImage;
    [SerializeField]
    private Image fillImage;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.OnValueChangedAsObservable()
            .Subscribe(value =>
            {
                fillImage.fillAmount = value/ slider.maxValue;
            });
    }
}
