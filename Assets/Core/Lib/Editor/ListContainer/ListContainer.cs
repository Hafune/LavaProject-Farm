using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements;

namespace Lib.Items
{
    public class ListContainer : VisualElement
    {
        public ListContainer([CanBeNull] IEnumerable<VisualElement> elements = null)
        {
            if (elements != null)
                Elements = elements;
        }

        public IEnumerable<VisualElement> Elements
        {
            set
            {
                Clear();
                value.ForEach(Add);
            }
        }
    }
}