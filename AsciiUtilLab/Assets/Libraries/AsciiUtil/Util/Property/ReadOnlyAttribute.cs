using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // 編集を無効化
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true; // 編集を再度有効化
    }
}