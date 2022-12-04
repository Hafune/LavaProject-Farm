using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LookAtCamera : MonoBehaviour
{
    private Camera _uiCamera;
    
    private void Start() => _uiCamera = GetComponent<Canvas>().worldCamera;

    private void Update() => transform.rotation = _uiCamera.transform.rotation;
}