using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem
{
    public static void SaveLevelsData(LevelsDataStorage lvData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levelsData.fun";
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, lvData);
        }

        //Debug.Log(lvData.levels[0].isUnlocked);
    }

    public static LevelsDataStorage LoadLevelsData()
    {
        string path = Application.persistentDataPath + "/levelsData.fun";
        //File.Delete(path); 
        
        if (File.Exists(path))
        {
            if (new FileInfo(path).Length > 0)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Open)) 
                {
                    try
                    {
                        LevelsDataStorage data = formatter.Deserialize(stream) as LevelsDataStorage;
                        data.fillEmptyLevels();
                        return data;
                    }
                    catch (System.Runtime.Serialization.SerializationException e)
                    {
                        Debug.LogError("Error deserializing data: " + e.Message);
                        return CreateNewLevelsData(path);
                    }
                }
            }
            else
            {
                Debug.LogError("The save file is empty: " + path);
                return CreateNewLevelsData(path);
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return CreateNewLevelsData(path);
        }
    }

    private static LevelsDataStorage CreateNewLevelsData(string path)
    {
        LevelsDataStorage data = new LevelsDataStorage();
        data.fillEmptyLevels();
        SaveLevelsData(data);
        return data;
    }
}
