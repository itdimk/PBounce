using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private const string DifficultyKey = "difficulty";
    private const string MusicVolumeKey = "music-volume";
    private const string EffectsVolumeKey = "effects-volume";
    
    public AudioManager Audio;

    public int Difficulty
    {
        get => PlayerPrefs.GetInt(DifficultyKey);
        set => PlayerPrefs.SetInt(DifficultyKey, value);
    }

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat(MusicVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(MusicVolumeKey, value);
            ApplyToAudioManager();
        }
    }

    public float EffectsVolume
    {
        get => PlayerPrefs.GetFloat(EffectsVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(EffectsVolumeKey, value);
            ApplyToAudioManager();
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(EffectsVolumeKey))
            EffectsVolume = 1;
        
        if (!PlayerPrefs.HasKey(MusicVolumeKey))
            MusicVolume = 1;
        
        ApplyToAudioManager();
    }

    private void ApplyToAudioManager()
    {
        if (Audio != null)
        {
            foreach (var sound in Audio.Sounds.Where(s => s.Category == Sound.SoundCategory.Music))
                sound.VolumeScale = MusicVolume;
            
            foreach (var sound in Audio.Sounds.Where(s => s.Category == Sound.SoundCategory.Effects))
                sound.VolumeScale = EffectsVolume;
        }
    }
}