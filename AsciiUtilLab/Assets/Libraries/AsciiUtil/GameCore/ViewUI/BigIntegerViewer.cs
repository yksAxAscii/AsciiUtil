using System.Numerics;
using UnityEngine;
using TMPro;
using UniRx;

public class BigIntegerViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private ulong value;
    [SerializeField]
    private ulong plus;


    void Start()
    {
        this.ObserveEveryValueChanged(_ => value)
            .Subscribe(_ => text.text = ConvertNumberToUnit(value));
    }

    void Update()
    {
        value += plus;
    }

    private string ConvertNumberToUnit(ulong number)
    {
        const float b = 1000000000;
        const float m = 1000000;
        const float k = 1000;

        if (number >= b)
        {
            return (number / b).ToString("0.00") + "B";
        }
        else if (number >= m)
        {
            return (number / m).ToString("0.00") + "M";
        }
        else if (number >= k)
        {
            return (number / k).ToString("0.00") + "K";
        }
        else
        {
            return number.ToString();
        }
    }
}
