using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lib.Items
{
    public static class Item
    {
        private static readonly VisualTreeAsset elem = UxmlNode.loadElem();
        
        public static VisualElement BuildElem(ItemData data)
        {
            var e = elem.CloneTree().Q<VisualElement>();

            e.Q<Label>("Name").text = data.name;
            e.Q<Label>("Src").text = data.src;
            e.Q<Button>("Button").clicked += () => System.Diagnostics.Process.Start(
                EditorPrefs.GetString("kScriptsDefaultApp", ""),
                $"{Application.dataPath}/{data.src}.cs");

            return e;
        }
    }
}