using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HelpTheNpcQuest : Quest
{
    [HideInInspector]
    public List<Enemy> enemies= new List<Enemy>();



    public override bool CheckTheQuest()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.IsAlive)
            {
                enemies.Remove(enemy);
            }
        }

        Debug.LogWarning("remaining: "+ enemies.Count);
        if (enemies.Count > 0)
            return false;
        else
        {
            CompleteTheQuest();
            return true;
        }
            
    }

 

    

    public override void GetQuest()
    {
        print("GetFromLoad");
       this.QuestDetails.QuestDescriptionKey = "Help_The_Npc_Quest_Description";
        this.QuestDetails.QuestNameKey = "HelpTheNpcQuestName";


      //  Debug.LogWarning("remaining: " + enemies.Count);
        foreach (EventArea Event in EventManager.Instance.events)
        {
            if (Event.EventName == "AltarEvent")
            {
                AltarEvent altarEvent = (AltarEvent)Event;

                Debug.LogWarning("find it");
                enemies = altarEvent.Enemies;
                 print("!!!!!!!!"+altarEvent.Enemies.Count);
            }
        }
    }
}
