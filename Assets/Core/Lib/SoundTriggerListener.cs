using Lib;
using UnityEngine;

[RequireComponent(typeof(ISoundTrigger))]
public class SoundTriggerListener : MonoConstruct
{
    [SerializeField] private AudioClip _clip;

    private SoundController _controller;
    private ISoundTrigger _trigger;

    protected override void Construct(IMyContext context)
    {
        _controller = context.Resolve<SoundController>();
        _trigger = GetComponent<ISoundTrigger>();
        _trigger.OnSoundPlay += SoundPlay;
    }

    private void OnDestroy() => _trigger.OnSoundPlay -= SoundPlay;

    private void SoundPlay() => _controller.PlayEffect(_clip);
}