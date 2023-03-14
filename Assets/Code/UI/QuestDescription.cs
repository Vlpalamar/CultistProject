using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDescription : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI questDescription;

    public TextMeshProUGUI Description { get => questDescription; set => questDescription = value; }

    public void SetTextDescription(string questDescripton)
    {
        questDescription.text = questDescripton;
    }

}
