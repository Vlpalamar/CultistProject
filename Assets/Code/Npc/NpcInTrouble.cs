using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInTrouble : NPC
{
    protected override void AddQuests()
    {
        HelpTheNpcQuest quest = new HelpTheNpcQuest();
      
        quest.QuestNameKey = "HelpTheNpcQuest_Name";
        quest.QuestDescriptionKey= "HelpTheNpcQuest_Description";
        quests.Add(quest);
        
    }

   
}
