using UnityEngine;

public class AudioManagerProxy : MonoBehaviour
{
    private AudioManager _audio;

    private AudioManager Audio => _audio ? _audio : _audio = FindObjectOfType<AudioManager>();

    public void Play(string name) => Audio.Play(name);

    public void Stop(string name) => Audio.Stop(name);

    public void PlayOneShot(string name) => Audio.PlayOneShot(name);

}
