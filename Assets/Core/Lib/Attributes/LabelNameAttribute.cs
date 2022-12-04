using UnityEngine;

namespace Lib
{
    public class LabelNameAttribute : PropertyAttribute
    {
        public readonly string Name;

        public LabelNameAttribute(string name) => Name = name;
    }
}