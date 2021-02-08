using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    private PlayerData _data = new PlayerData();

    public int GreenRubies
    {
        get => _data.GreenRubies;
        set => _data.GreenRubies = value;
    }

    public int RedRubies
    {
        get => _data.RedRubies;
        set => _data.RedRubies = value;
    }

    private void Start()
    {
        throw new NotImplementedException();
    }


    public void Load()
    {
        
        string path = Application.persistentDataPath + "\\saves.dat";
        var formatter = new BinaryFormatter();

        if(!File.Exists(path)) return;

        using (var stream = File.OpenRead(path))
            _data = (PlayerData) formatter.Deserialize(stream);
    }

    public void Save()
    {
        string path = Application.persistentDataPath + "\\saves.dat";
        var formatter = new BinaryFormatter();

        using (var stream = File.OpenWrite(path))
            formatter.Serialize(stream, _data);
    }

    public void AddGreenRuby()
    {
        GreenRubies++;
    }
    
    public void AddRedRuby()
    {
        RedRubies++;
    }
}