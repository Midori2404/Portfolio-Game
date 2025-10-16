using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Audio
{
    [HideInInspector] private AudioSource _source;

    public AudioMixerGroup audioMixer;
    public AudioClip clip;

    public string Clipname;
    public bool loop;
    public bool PlayOnAwake;

    [Range(.3f, 2f)]
    public float volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public void SetSource(AudioSource Source)
    {
        _source = Source;
        if (clip == null)
        {
            Debug.LogWarning($"Audio '{Clipname}' has no clip assigned!");
            return;
        }

        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = Pitch;
        _source.loop = loop;
        _source.playOnAwake = PlayOnAwake;
        _source.outputAudioMixerGroup = audioMixer;

        Debug.Log($"SetSource success for '{Clipname}'");
    }

    public void Play()
    {
        if (_source != null)
        {
            Debug.Log($"Playing audio: {Clipname}");
            _source.Play();
        }
        else
        {
            Debug.LogWarning($"AudioSource not set for: {Clipname}");
        }
    }

    public void Stop()
    {
        if (_source != null)
        {
            _source.Stop();
        }
    }

    public void SetVolume(float newVolume)
    {
        if (_source != null)
        {
            _source.volume = newVolume;
        }
    }
}
