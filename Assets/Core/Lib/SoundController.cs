using System.Collections;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _background;
    [SerializeField] private AudioSource _playEffect;

    public void PlayBackground() => _background.Play();

    public void PlayEffect(AudioClip clip) => _playEffect.PlayOneShot(clip);

    private void OnGameLoaded() => PlayBackground();

    private void OnResultScreenShowed() => StartCoroutine(LowerVolume());

    private IEnumerator LowerVolume()
    {
        while (_background.volume > .2f)
        {
            _background.volume -= Time.deltaTime;

            yield return null;
        }

        _background.volume = .2f;
    }

    private IEnumerator UpperVolume()
    {
        while (_background.volume < 1f)
        {
            _background.volume += Time.deltaTime;

            yield return null;
        }
    }
}