using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    private void Awake()
    {
        foreach (Sound sound in Sounds)
            sound.Apply(gameObject.AddComponent<AudioSource>());
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);

        if (s != null)
            s.Source.Play();
        else
            Debug.LogWarning($"Sound \"{name}\" doesn't exist");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);

        if (s != null)
            s.Source.Stop();
        else
            Debug.LogWarning($"Sound \"{name}\" doesn't exist");
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);

        if (s != null)
            s.Source.PlayOneShot(s.Clip, s.Volume);
        else
            Debug.LogWarning($"Sound \"{name}\" doesn't exist");
    }
}