using UnityEngine;

public class SoundPlayer : MonoBehaviour, IPauseHandler
{
    [SerializeField] private AudioSource _audioSource;

    public void Init()
    {
        GameContext.Instance.PauseManager.RegisterPauseListener(this);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
        _audioSource.volume = 0.0f;
    }

    public void ChangeVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void SetAudioLoopStatus(bool isLooped)
    {
        _audioSource.loop = isLooped;
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            _audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        GameContext.Instance.PauseManager.UnRegisterPauseListener(this);
    }
}