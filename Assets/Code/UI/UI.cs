using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : SingletonMonoBehaviour<UI>
{
    #region Pause Menu
    [Space(5)]
    [Header("Pause")]
    #endregion
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Slider _musicVolume;

    #region Quest Menu
    [Space(5)]
    [Header("Quests Menu")]
    #endregion
    [SerializeField] private VerticalLayoutGroup questLayoutGroup;
    [SerializeField] private GameObject questDescriptionPrefab;
    [SerializeField] private int amountOfQuestsOnScreen = 5;
    private List<QuestDescription> questDescriptions = new List<QuestDescription>();

    #region GamePlay Elements
    [Space(5)]
    [Header("GamePlay Elements")]
    #endregion
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider exhastedHpBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private Slider exhastedStaminaBar;

    #region GamePlay Elements
    [Space(5)]
    [Header("UI Details")]
    #endregion
    [SerializeField] private float secondsUntilBarExosted = 0.5f;
    [SerializeField] private float barExhastedMultiplier = 30f;
    #region SceneName
    [Space(5)]
    [Header("SceneName")]
    #endregion
    [SerializeField] private string thisSceneName;
    [SerializeField] private string mainaMenuSceneName;


    private const string soundValueKey= "soundValueKey";
    private const string musicValueKey = "musicValueKey";


    public Slider HpBar { get => hpBar;  }
    public Slider StaminaBar { get => staminaBar; }
    public Slider ExhastedStaminaBar { get => exhastedStaminaBar; }
    public Slider ExhastedHpBar { get => exhastedHpBar;  }
 
    public float BarExhastedMultiplier { get => barExhastedMultiplier;  }
    public float SecondsUntilBarExosted { get => secondsUntilBarExosted;  }
    public GameObject DeathMenu { get => deathMenu; }

    protected  void Start()
    {
        
        Initialise();
    }

   

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)&& !_pauseMenu.activeSelf)
        {
            OpenPauseMenu();
            return;
        }
            

        if (Input.GetKeyDown(KeyCode.Escape) && _pauseMenu.activeSelf)
        {
            ClosePauseMenu();
            return;
        }
            


    }

    private void Initialise()
    {
        if (PlayerPrefs.HasKey(soundValueKey))
            SoundEffectManager.Instance.SoundVolume = PlayerPrefs.GetInt(soundValueKey);

        if (PlayerPrefs.HasKey(musicValueKey))
            MusicManager.Instance.MusicVolume = PlayerPrefs.GetInt(musicValueKey); 


        _soundVolume.value = SoundEffectManager.Instance.SoundVolume;

        _musicVolume.value = MusicManager.Instance.MusicVolume;


        RefillTheQuestLayoutGroup();
    }

    public void ChangeSoundVolume()
    {
        SoundEffectManager.Instance.ChangeSoundsVolume((int)_soundVolume.value);

        PlayerPrefs.SetInt(soundValueKey, (int)_soundVolume.value);
    }

    public void ChangeMusicVolume()
    {
        MusicManager.Instance.ChangeSoundsVolume((int)_musicVolume.value);

        PlayerPrefs.SetInt(musicValueKey, (int)_musicVolume.value);
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
    }

    public void AddQuestDescriptionToUI(string questDescription)
    {
       

        GameObject description= Instantiate(questDescriptionPrefab);
        QuestDescription newDescription = description.GetComponent<QuestDescription>();
        newDescription.SetTextDescription(questDescription);
        newDescription.transform.parent = questLayoutGroup.transform;


        questDescriptions.Add(newDescription);

        if (questDescriptions.Count>amountOfQuestsOnScreen)
            RefillTheQuestLayoutGroup();
        
    }

    public void DeleteQuestDescriptionOfUI(string questDescription)
    {
        foreach (QuestDescription quest in questDescriptions)
        {
            if (quest.Description.text==questDescription)
                StartCoroutine(StartDeletingQuestRoutine(quest));
            
        }
    }

    private IEnumerator StartDeletingQuestRoutine(QuestDescription quest)
    {
        RectTransform rectTransform = quest.gameObject.GetComponent<RectTransform>();
        float x = rectTransform.localScale.x;
        float y = rectTransform.localScale.y;
        do
        {
            
            x -=Time.deltaTime;
            y -= Time.deltaTime;
            rectTransform.localScale = new Vector3(x,y);
            yield return new WaitForFixedUpdate();

        } while (rectTransform.localScale.x>0 || rectTransform.localScale.y > 0);
        yield return new WaitForFixedUpdate();
        questDescriptions.Remove(quest);
        Destroy(quest.gameObject);
    }

    private void RefillTheQuestLayoutGroup()
    {
        int j = 0;
        for (int i = questDescriptions.Count - 1; i > 0; i--)
        {
            if (j < amountOfQuestsOnScreen)
            {
                questDescriptions[i].transform.parent = questLayoutGroup.transform;
            }
            else
            {
                questDescriptions[i].gameObject.SetActive(false);
            }

        }
    }


    public void Replay()
    {
        SceneManager.LoadScene(thisSceneName);
    }
    public void GoToMainManu()
    {
        SceneManager.LoadScene(mainaMenuSceneName);
    }
}
