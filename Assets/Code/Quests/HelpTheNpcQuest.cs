using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HelpTheNpcQuest : Quest
{
    [HideInInspector]
    public List<Enemy> enemies= new List<Enemy>();
    private AltarEvent _altarEvent;
    



    public override bool CheckTheQuest()
    {
        if (!_altarEvent.IsActive) return false;
       

        
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.IsAlive)
            {
                enemies.Remove(enemy);

            }
        }

     
        if (enemies.Count > 1)
            return false;
        else
        {
           
            CompleteTheQuest();
            return true;
        }
            
    }


    protected override void CompleteTheQuest()
    {
        base.CompleteTheQuest();
        _altarEvent.CompleteEvent();
       
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
               
                _altarEvent = (AltarEvent)Event;

                if (!_altarEvent.IsActive) return;
                

                
                Debug.LogWarning("find it");
                enemies = _altarEvent.Enemies;
                
            }
        }
    }
}
