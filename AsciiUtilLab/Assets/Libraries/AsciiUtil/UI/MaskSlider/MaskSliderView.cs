using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マスクを使ったスライダー
/// </summary>
public class MaskSliderView : MonoBehaviour
{
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Slider slider;
    private float maxValue = 1.0f;

    private void Start()
    {
        slider.fillRect = null;
        SetValue(slider.value);
        slider.onValueChanged.AddListener(SetValue);
    }

    // スライダーの値を設定するメソッド
    private void SetValue(float value)
    {
        fillImage.fillAmount = value / maxValue;
    }
}
