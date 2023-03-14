using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager 
{
    //private  const string QuestListSavePath= "/QuestList.dat";

    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        PlayerPrefs.SetString(key, jsonDataString);
        PlayerPrefs.Save();
    }






    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadString = PlayerPrefs.GetString(key);

            return JsonUtility.FromJson<T>(loadString);
        }
        else return new T();
    }





    //public static void Save( List<Quest> activequests, List<Quest> complitedQuests)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + QuestListSavePath;
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    QuestData data = new QuestData(activequests, complitedQuests);
    //    formatter.Serialize(stream, data);

    //    stream.Close();


    //}


    //public static QuestData Load()
    //{
    //    string path = Application.persistentDataPath + QuestListSavePath;
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream straem = new FileStream(path, FileMode.Open);

    //        QuestData data=  formatter.Deserialize(straem) as QuestData  ;
    //        straem.Close();
    //        return data;
    //    }
    //    else
    //    {
    //        Debug.LogError("SaveFileNotFound: "+ QuestListSavePath);
    //        return null;
    //    }
    //}

}
