using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(Animator))]
public class LookAtCameraEffect : MonoBehaviour
{
    private Camera _uiCamera;
    
    private void Start() => _uiCamera = GetComponent<Canvas>().worldCamera;

    private void Update() => transform.rotation = _uiCamera.transform.rotation;
    
    //Метод для аниматора
    private void OnAnimationEnded() => gameObject.SetActive(false);
}