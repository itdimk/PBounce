using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class StringListProviderX : MonoBehaviour
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
    public List<StringListEntry> StringLists;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
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