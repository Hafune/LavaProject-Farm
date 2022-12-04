using Lib;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasWorldUICameraSetup : MonoConstruct
{
    protected override void Construct(IMyContext context)
    {
        GetComponent<Canvas>().worldCamera = context.Resolve<UICameraMarker>().GetComponent<Camera>();
        Destroy(this);
    }
}