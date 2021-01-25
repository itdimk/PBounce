using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerProxy : MonoBehaviour
{
    private AudioManager _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
    }

    public void Play(string name) => _audio.Play(name);

    public void Stop(string name) => _audio.Stop(name);

    public void PlayOneShot(string name) => _audio.PlayOneShot(name);

}
