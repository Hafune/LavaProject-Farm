using System.Collections;
using UnityEngine;

public class FocusBehavior : MonoBehaviour
{
    [SerializeField] private CameraTarget _cameraTarget;

    public void BeginFocus() => StartCoroutine(LookAt());

    private IEnumerator LookAt()
    {
        yield return new WaitForSeconds(2f);

        _cameraTarget.SetHighPriority();

        yield return new WaitForSeconds(2f);

        _cameraTarget.SetLowPriority();
    }
}