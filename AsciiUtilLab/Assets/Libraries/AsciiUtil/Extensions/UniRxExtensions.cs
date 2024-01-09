using UnityEngine;
using UniRx;
#if UNITY_EDITOR
using UnityEditor;
[CustomPropertyDrawer(typeof(IntReactiveProperty)),
 CustomPropertyDrawer(typeof(FloatReactiveProperty)),
 CustomPropertyDrawer(typeof(Vector2ReactiveProperty)),
 CustomPropertyDrawer(typeof(BoolReactiveProperty))
]
public class AddInspectorDisplayDrawer : InspectorDisplayDrawer
{ }
#endif
#region ViewInspectorReactiveProperty
/// <summary>
/// int型のReactivePropertyをインスペクターに表示するためのクラス
/// </summary>
[System.Serializable]
public class IntReactiveProperty : ReactiveProperty<int>
{
    public IntReactiveProperty(int value = default) : base(value)
    {

    }
}
/// <summary>
/// float型のReactivePropertyをインスペクターに表示するためのクラス
/// </summary>
[System.Serializable]
public class FloatReactiveProperty : ReactiveProperty<float>
{
    public FloatReactiveProperty(float value = default) : base(value)
    {

    }
}
/// <summary>
/// Vector2型のReactivePropertyをインスペクターに表示するためのクラス
/// </summary>
[System.Serializable]
public class Vector2ReactiveProperty : ReactiveProperty<Vector2>
{
    public Vector2ReactiveProperty(Vector2 value = default) : base(value)
    {

    }
}
/// <summary>
/// bool型のReactivePropertyをインスペクターに表示するためのクラス
/// </summary>
[System.Serializable]
public class BoolReactiveProperty : ReactiveProperty<bool>
{
    public BoolReactiveProperty(bool value = default) : base(value)
    {

    }
}
#endregion
