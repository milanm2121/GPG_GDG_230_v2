using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class save_system
{
    static string Path = Application.persistentDataPath + "/item_Savedata.txt";

    public static void saveData(temp_collection tempc)
    {
        Debug.Log(Path);
        BinaryFormatter formatter = new BinaryFormatter();
        Path = Application.persistentDataPath + "/collection_Savedata.txt";

        FileStream stream = new FileStream(Path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, tempc);

        stream.Close();

    }
    public static temp_collection LoadSaveData()
    {
        if (!File.Exists(Path))
        {
            return null;
        }


        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(Path, FileMode.Open);

        temp_collection data = formatter.Deserialize(stream) as temp_collection;

        stream.Close();

        data.temp_collection_load();


        return data;


    }
}
