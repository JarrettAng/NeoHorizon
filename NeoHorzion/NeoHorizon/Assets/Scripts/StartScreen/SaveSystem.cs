using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static void SaveHighscoreData(List<PlayerInfo> currentInfo) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.dat", FileMode.Create);

        bf.Serialize(stream, currentInfo);

        stream.Close();
    }

    public static List<PlayerInfo> LoadHighscoreData() {
        List<PlayerInfo> storedInfo = new List<PlayerInfo>();

        if(File.Exists(Application.persistentDataPath + "/PlayerData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            storedInfo = bf.Deserialize(stream) as List<PlayerInfo>;

            stream.Close();
        }

        return storedInfo;
    }

    public static void SaveCurrentName(string currentName) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/CurrentName.dat", FileMode.Create);

        bf.Serialize(stream, currentName);

        stream.Close();
    }

    public static string LoadCurrentName() {
        string storedName = "AAA";

        if(File.Exists(Application.persistentDataPath + "/CurrentName.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/CurrentName.dat", FileMode.Open);
            storedName = bf.Deserialize(stream) as string;

            stream.Close();
        }

        return storedName;
    }
}
