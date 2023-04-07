using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInTrouble : NPC
{
    private List<string> _quests = new List<string>();
    protected override void AddQuests()
    {
        _quests.Add("HelpTheNpcQuestName");
       
       
      
        foreach (QuestSO item in GameManager.Instance.QuestsPool.AllQuests)
        {
            foreach (string questName in _quests)
            {
                if (questName==item.QuestNameKey)
                {
                  
                    quests.Add(item.Quest);
                }
            }
        }

        //print("Quest is Ready TO Take");


    }


}
