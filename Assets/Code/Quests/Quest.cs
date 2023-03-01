using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest 
{
    private string questNameKey;
    private string questDescriptionKey;

  


 
    public string QuestNameKey { get => questNameKey; set => questNameKey = value; }
    public string QuestDescriptionKey { get => questDescriptionKey; set => questDescriptionKey = value; }

    public abstract bool CheckTheQuest();

    public abstract void GetQuest();

    protected void CompleteTheQuest()
    {
        //мелодия
    }
}
