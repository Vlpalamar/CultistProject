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

    public void CheckQuests()
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.CheckTheQuest())
            {
                CompleteQuest(quest);
            }
        }
    }

    public void TakeQuest(Quest quest)
    {
        activeQuests.Add(quest);
        quest.GetQuest();

        if (takeTheQuestSE != null)
            SoundEffectManager.Instance.PlaySoundEffect(takeTheQuestSE);
            
        CheckQuests();
    }

    public void CompleteQuest (Quest quest)
    {
        Debug.LogWarning("You have completed the quest");

        if (CompleteTheQuestSE != null)
            SoundEffectManager.Instance.PlaySoundEffect(CompleteTheQuestSE);

        activeQuests.Remove(quest);
        completedQuests.Add(quest);
    }
}
