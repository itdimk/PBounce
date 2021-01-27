using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TranslatedStringList : MonoBehaviour
{
    [Serializable]
    public class StringListEntry
    {
        public string Language;
        [TextArea] public List<string> StringList;
        public override string ToString() => Language;
    }

    private SettingsManager _manager;
    [SerializeField] private List<StringListEntry> StringLists;

    public IReadOnlyList<string> ResultStringList => GetTranslatedStringList();

    private void Start()
    {
        _manager = FindObjectOfType<SettingsManager>();

        if (_manager == null)
            Debug.LogError($"Can't find {nameof(SettingsManager)} on the scene");
    }

    private IReadOnlyList<string> GetTranslatedStringList()
    {
        string language = _manager.Language;
        var entry = StringLists.FirstOrDefault(s => s.Language == language);

        if (entry != null) return entry.StringList;

        Debug.LogWarning($"String list isn't specified for the language \"{language}\"");
        return new List<string>();
    }
}