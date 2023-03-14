using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerQuests : MonoBehaviour
{
    #region Audio
    [Space(5)] 
    [Header("Audio")]
    #endregion
    #region Tooltip
   [Tooltip("Populate with sound, that will play with taking quests")]

    #endregion
    [SerializeField] private SoundEffectSO takeTheQuestSE;
    #region Tooltip
    [Tooltip("Populate with sound, that will play with endig quests")]

    #endregion
    [SerializeField] private SoundEffectSO CompleteTheQuestSE;

    List<Quest> activeQuests = new List<Quest>();
    List<Quest> completedQuests = new List<Quest>();

    private const string KeyToSaveQuests = "KeyToSaveActiveQuests";

    private LocalizationManager _localizationManager;
    private Player _player;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (_localizationManager == null)
            _localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();

        _player = GameManager.Instance.GetPlayer();

        //PlayerPrefs.DeleteAll();

        Invoke(nameof(LoadQuestsData), 0.1f);
        //LoadQuestsData();

       
    }

    private void LoadQuestsData()
    {

        QuestData data = SaveManager.Load<QuestData>(KeyToSaveQuests);
        if (data == null) return;
        if (data.completedQuests == null && data.activeQuests == null) return;
        
        if (data.activeQuests.Count!=0)
        {
            foreach (string questName in data.activeQuests)
            {
                foreach (QuestSO item in GameManager.Instance.QuestsPool.AllQuests)
                {
                    if (questName == item.QuestNameKey)
                    {
                        print(item.Quest);
                        item.Quest.GetQuest();
                        activeQuests.Add(item.Quest);
                       
                    }
                }
            }
        }
        else
            Debug.LogWarning("There are no active quests in data");
        

        if (data.completedQuests.Count!= 0)
        {
            foreach (string questName in data.completedQuests)
            {
                foreach (QuestSO item in GameManager.Instance.QuestsPool.AllQuests)
                {
                    if (questName == item.QuestNameKey)
                    {
                        completedQuests.Add(item.Quest);
                    }
                }

            }
        }
        else
            Debug.LogWarning("There are no completed quests in data");


        print("completedQuests" + completedQuests.Count);
        print("activeQuests" + activeQuests.Count);

        foreach (Quest quest in activeQuests)
        {
            _player.UI.AddQuestDescriptionToUI(_localizationManager.GetLocalizedValue(quest.QuestDetails.QuestDescriptionKey));
        }
    }

    public void CheckQuests()
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.CheckTheQuest())
            {
                CompleteQuest(quest);
            }
        }

        SaveQuests();
    }

    public void TakeQuest(Quest quest)
    {
        activeQuests.Add(quest);
        quest.GetQuest();

        if (takeTheQuestSE != null)
            SoundEffectManager.Instance.PlaySoundEffect(takeTheQuestSE);

        _player.UI.AddQuestDescriptionToUI(_localizationManager.GetLocalizedValue(quest.QuestDetails.QuestDescriptionKey));

        CheckQuests();
      
    }

    private void SaveQuests()
    {

        List<string> active = new List<string>();
        foreach (Quest quest in activeQuests)
        {
            active.Add(quest.QuestDetails.QuestNameKey);
        }

        List<string> completed = new List<string>();
        foreach (Quest quest in completedQuests)
        {
            completed.Add(quest.QuestDetails.QuestNameKey);
        }

        QuestData questData = new QuestData(active, completed);


       
        SaveManager.Save(KeyToSaveQuests, questData);

       
        

    }



   


    public void CompleteQuest (Quest quest)
    {
        Debug.LogWarning("You have completed the quest");

        if (CompleteTheQuestSE != null)
            SoundEffectManager.Instance.PlaySoundEffect(CompleteTheQuestSE);

        _player.UI.DeleteQuestDescriptionOfUI(_localizationManager.GetLocalizedValue(quest.QuestDetails.QuestDescriptionKey));

        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        
    }
}
