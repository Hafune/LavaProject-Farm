using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(Animator))]
public class EffectWithCount : MonoBehaviour
{
    [SerializeField] private LabelView _labelView;
    [SerializeField] private Animator _animator;

    private Camera _uiCamera;
    private int _clipHash;

    public LabelView LabelView => _labelView;

    private void Awake() => _clipHash = Animator.StringToHash(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

    private void Start() => _uiCamera = GetComponent<Canvas>().worldCamera;

    public void RestartAnimation() => _animator.Play(_clipHash, 0, 0);

    private void Update() => transform.rotation = _uiCamera.transform.rotation;

    //Метод для аниматора
    private void OnAnimationEnded() => gameObject.SetActive(false);
}