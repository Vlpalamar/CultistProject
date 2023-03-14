using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public const string LANGUAGE = "Language";

    private string currentLanguage;
    private bool isReady;
    private Dictionary<string, string> localizedText;

    public string CurrentLanguage
    {
        get
        {
            return currentLanguage;

        }
        set
        {
            
            //CurrentLanguage = value;
            LoadLocalizedText(value);
            PlayerPrefs.SetString(LANGUAGE, value);
        }
    }

    public bool IsReady
    {
        get
        {
            return isReady;
        }
    }

    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;

    private void Awake()
    {
       
        if (!PlayerPrefs.HasKey(LANGUAGE))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString(LANGUAGE, "ru_RU");
            }
            else
            {
                PlayerPrefs.SetString(LANGUAGE, "en_US");
            }
        }

       // PlayerPrefs.SetString(LANGUAGE, "en_US");
        currentLanguage = PlayerPrefs.GetString(LANGUAGE);

        LoadLocalizedText(currentLanguage);
    }


    

    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";
       // print(path);

        string dataAsJson;

        dataAsJson = File.ReadAllText(path);

        

        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    WWW reader = new WWW(path);
        //    while (!reader.isDone) { }

        //    dataAsJson = reader.text;
        //}
        //else
        //{
        //    dataAsJson = File.ReadAllText(path);
        //}


        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        PlayerPrefs.SetString(LANGUAGE, langName);
        isReady = true;
        OnLanguageChanged?.Invoke();
    }
    public string GetLocalizedValue(string key)
    {
        //if (localizedText==null)
        //{
        //    LoadLocalizedText(currentLanguage);
        //}

        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            throw new Exception("Localized text with key \"" + key + "\" not found");
        }
    }

}