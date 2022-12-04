using UnityEngine.UIElements;

namespace Lib.Items
{
    public static class SearchField
    {
        private static readonly VisualTreeAsset elem = UxmlNode.loadElem();

        public static VisualElement BuildElem(EventCallback<ChangeEvent<string>> onChange)
        {
            var textField = elem.CloneTree().Q<TextField>();
            textField.RegisterCallback(onChange);
            return textField;
        }
    }
}