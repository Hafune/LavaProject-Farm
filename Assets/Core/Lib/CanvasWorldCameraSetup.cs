using Lib;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasWorldCameraSetup : MonoConstruct
{
    protected override void Construct(IMyContext context)
    {
        GetComponent<Canvas>().worldCamera = context.Resolve<Camera>();
        Destroy(this);
    }
}