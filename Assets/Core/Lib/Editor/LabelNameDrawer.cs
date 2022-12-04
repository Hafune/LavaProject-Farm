using Lib;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelNameAttribute))]
public class LabelNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property,
            new GUIContent(((LabelNameAttribute) attribute).Name + $" ({property.displayName})"));
    }
}