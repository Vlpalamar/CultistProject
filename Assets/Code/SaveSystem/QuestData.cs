using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData 
{
    public List<string> activeQuests;
    public List<string> completedQuests;

    public QuestData(List<string> active, List<string>  completed)
    {
        activeQuests = active;
        completedQuests = completed;
    }
    public QuestData() { }
}
