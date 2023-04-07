using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Quest :MonoBehaviour
{

    [SerializeField] private QuestSO questDetails;

    public QuestSO QuestDetails { get => questDetails; set => questDetails = value; }

   


 
    
    public abstract bool CheckTheQuest();

    public abstract void GetQuest();

    protected virtual void CompleteTheQuest()
    {
        //мелодия
    }
}
