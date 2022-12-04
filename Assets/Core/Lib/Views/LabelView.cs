using Lib;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LabelView : MonoBehaviour
{
    [SerializeField] private MyLabel myLabel;

    private void OnValidate() => myLabel.OnValidate(this);

    public void SetText(string value) => myLabel.SetText(value);

    public void FormatText<T>(params T[] args) => myLabel.FormatText(args);

    public void FormatText(int arg0) => myLabel.FormatText(arg0);
}