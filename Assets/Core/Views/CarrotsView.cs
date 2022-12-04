using UnityEngine;

namespace Core.Views
{
    public class CarrotsView : MonoBehaviour
    {
        [SerializeField] private LabelView _label;

        private void OnValidate() => _label = _label ??= GetComponentInChildren<LabelView>();

        public LabelView Label => _label;
    }
}