using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinarySaveSystem 
{
    private readonly string _filePath;

    public BinarySaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.dat";
    }

    public void Save(SaveData data)
    {
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, data);
        }
    }

    public SaveData Load()
    {
        if(File.Exists(_filePath))
        {
            SaveData saveData;
            using (FileStream file = File.Open(_filePath, FileMode.Open))
            {
                object loadedData = new BinaryFormatter().Deserialize(file);
                saveData = (SaveData)loadedData;
            }
            return saveData;
        }
        return new SaveData();
    }
}