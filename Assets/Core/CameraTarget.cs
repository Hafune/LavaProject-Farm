using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraTarget : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachine;

    public void SetHighPriority() => _cinemachine.Priority = 100;

    public void SetLowPriority() => _cinemachine.Priority = 0;

    private void Awake() => _cinemachine = GetComponent<CinemachineVirtualCamera>();
}