using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SettingsManager : MonoBehaviour
{
    private AudioManager _audio;

    private const string DifficultyKey = "difficulty";
    private const string MusicVolumeKey = "music-volume";
    private const string EffectsVolumeKey = "effects-volume";
    private const string VoiceVolumeKey = "voice-volume";
    private const string LanguageKey = "language";

    public UnityEvent LanguageChanged;
    public UnityEvent DifficultyChanged;

    public string Language
    {
        get => PlayerPrefs.GetString(LanguageKey);
        set
        {
            PlayerPrefs.SetString(LanguageKey, value);
            LanguageChanged?.Invoke();
        }
    }

    public int Difficulty
    {
        get => PlayerPrefs.GetInt(DifficultyKey);
        set
        {
            PlayerPrefs.SetInt(DifficultyKey, value);
            DifficultyChanged?.Invoke();
        }
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

    public float VoiceVolume
    {
        get => PlayerPrefs.GetFloat(VoiceVolumeKey);
        set
        {
            PlayerPrefs.SetFloat(VoiceVolumeKey, value);
            ApplyToAudioManager();
        }
    }

    private void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
        SetDefaultsIfRequired();
        ApplyToAudioManager();
        
        if (FindObjectOfType<SettingsManager>() != this)
            Debug.LogWarning($"Scene can contain only one {nameof(SettingsManager)}");
    }

    private void SetDefaultsIfRequired()
    {
        if (!PlayerPrefs.HasKey(EffectsVolumeKey))
            EffectsVolume = 1;

        if (!PlayerPrefs.HasKey(MusicVolumeKey))
            MusicVolume = 1;
        
        if (!PlayerPrefs.HasKey(VoiceVolumeKey))
            VoiceVolume = 1;
    }

    private void ApplyToAudioManager()
    {
        if (_audio == null) return;
        foreach (var sound in _audio.Sounds.Where(s => s.Category == Sound.SoundCategory.Music))
            sound.VolumeScale = MusicVolume;

        foreach (var sound in _audio.Sounds.Where(s => s.Category == Sound.SoundCategory.Effects))
            sound.VolumeScale = EffectsVolume;

        foreach (var sound in _audio.Sounds.Where(s => s.Category == Sound.SoundCategory.Voice))
            sound.VolumeScale = VoiceVolume;
    }
}