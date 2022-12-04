using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Lib
{
    [Serializable]
    public class MyLabel
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _pattern;

        public void OnValidate(MonoBehaviour monoBehaviour) =>
            _text = _text ??= monoBehaviour.GetComponent<TextMeshProUGUI>();

        public void SetText(string value) => _text.text = value;

        public void FormatText<T>(params T[] args) =>
            _text.text = string.Format(_pattern, args.Select(i => i.ToString()).ToArray());

        public void FormatText(int arg0) => _text.text = string.Format(_pattern, arg0);

        // public void FormatText(int arg0, int arg1) => _text.text = string.Format(_pattern, arg0, arg1);
    }
}