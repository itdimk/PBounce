using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum SoundCategory
    {
        None,
        Music,
        Effects,
        Voice
    }

    [SerializeField]
    private float volume;
    
    [SerializeField]
    private float pitch;

    private AudioSource _source;

    public string Name;
    public SoundCategory Category;
    public AudioClip Clip;

    public float Volume
    {
        get => -volume;
        set
        {
            volume = value;
            if (Source != null)
                Source.volume = value;
        }
    }

    public float Pitch
    {
        get => pitch;
        set
        {
            pitch = value;
            if (Source != null)
                Source.pitch = value;
        }
    }
    
    public bool Loop;
    
    public AudioSource Source => _source;

    public void Apply(AudioSource target)
    {
        target.clip = Clip;
        target.volume = Volume;
        target.pitch = Pitch;
        target.loop = Loop;
        _source = target;
    }
}