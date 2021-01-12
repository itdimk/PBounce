using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StringProviderX : MonoBehaviour
{
    [Serializable]
    public class StringEntry
    {
        public string Language;
        
        [TextArea]
        public string String;

        public override string ToString() => Language;
    }

    public GameManagerX Manager;
    public List<StringEntry> StringList;
    public StringDisplay Output;
    
    // Start is called before the first frame update
    void Start()
    {
        if(Output != null)
            Output.SetItemToDisplay(GetString());
    }

    // Update is called once per frame
    void Update()
    {
    }

    string GetString()
    {
        string language = Manager.Language;
        var entry = StringList.FirstOrDefault(s => s.Language == language);

        if (entry != null)
            return entry.String;

        Debug.LogWarning($"String isn't specified for the language \"{language}\"");
        return string.Empty;
    }
}
