using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestDetails", menuName = "Scriptable Objects/Quest")]
public class QuestSO : ScriptableObject
{

    [SerializeField] private Quest quest;
    [SerializeField] private string questNameKey;
    [SerializeField] private string questDescriptionKey;

    public Quest Quest { get => quest; set => quest = value; }
    public string QuestNameKey { get => questNameKey; set => questNameKey = value; }
    public string QuestDescriptionKey { get => questDescriptionKey; set => questDescriptionKey = value; }
}
