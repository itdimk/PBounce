using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public Sound[] Sounds;


    private void Awake()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Sounds = Resources.FindObjectsOfTypeAll<Sound>()
            .Where(s => s.gameObject.scene.buildIndex == activeSceneIndex)
            .ToArray();
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
            s.Source.PlayOneShot(s.Source.clip);
        else
            Debug.LogWarning($"Sound \"{name}\" doesn't exist");
    }
}