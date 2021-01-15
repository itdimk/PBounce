using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TranslatedString : MonoBehaviour
{
    [Serializable]
    public class Entry
    {
        public string Language;

        [TextArea] public string String;

        public override string ToString() => Language;
    }

    public GameManagerX Manager;
    [SerializeField] private List<Entry> Translations;

    public string ResultString => GetTranslatedString();
    
    private string GetTranslatedString()
    {
        string language = Manager.Language;
        var entry = Translations.FirstOrDefault(s => s.Language == language);

        if (entry != null)
            return entry.String;

        Debug.LogWarning($"String isn't specified for the language \"{language}\"");
        return string.Empty;
    }
}