using System;
using UnityEngine;

[Serializable]
public class Sound : MonoBehaviour
{
    public enum SoundCategory
    {
        None,
        Music,
        Effects,
        Voice
    }

    private bool _initialized;
    private float _initVolume;
    private float _volumeScale = 1;

    public string Name;
    public AudioSource Source;
    public SoundCategory Category;

    public float VolumeScale
    {
        get => _volumeScale;
        set
        {
            if (!_initialized)
                Initialize();
            Source.volume = _initVolume * (_volumeScale = value);
        }
    }

    private void Initialize()
    {
        _initVolume = Source.volume;
        _initialized = true;
    }
}