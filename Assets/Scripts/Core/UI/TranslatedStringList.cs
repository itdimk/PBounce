using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TranslatedStringList : MonoBehaviour
{
    [Serializable]
    public class StringListEntry
    {
        public string Language;
        
        [TextArea]
        public List<string> Text;

        public override string ToString() => Language;
    }

    public GameManagerX Manager;
    [SerializeField] private List<StringListEntry> StringLists;

    public IReadOnlyList<string> ResultStringList { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        ResultStringList = GetTranslatedStringList();
    }
    
    public IReadOnlyList<string> GetTranslatedStringList()
    {
        string language = Manager.Language;
        var entry = StringLists.FirstOrDefault(s => s.Language == language);

        if (entry != null)
            return entry.Text;

        Debug.LogWarning($"String list isn't specified for the language \"{language}\"");
        return new List<string>(0);
    }
}