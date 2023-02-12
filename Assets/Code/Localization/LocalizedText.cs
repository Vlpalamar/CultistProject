using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string key;

    private LocalizationManager localizationManager;
    private TextMeshProUGUI text;

    void Start()
    {
        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        
        localizationManager.OnLanguageChanged += UpdateText;

        UpdateText();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
           
        }
        
    }

    private void OnDestroy()
    {
        localizationManager.OnLanguageChanged -= UpdateText;
    }

    virtual protected void UpdateText()
    {
        if (gameObject == null) return;

        if (localizationManager == null)
        {
            localizationManager = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>();
        }
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        text.text = localizationManager.GetLocalizedValue(key);
    }
}
