using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsPool : MonoBehaviour
{
    [SerializeField] private List<QuestSO> allQuests = new List<QuestSO>();

    public List<QuestSO> AllQuests { get => allQuests;  }
}
