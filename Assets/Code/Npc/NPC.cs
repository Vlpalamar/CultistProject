using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
[DisallowMultipleComponent]
public abstract class NPC : MonoBehaviour
{
    #region Header DialogBox Settings
    [Space(5)]
    [Header("DialogBox Settings")]
    #endregion

    #region Tooltip
    [Tooltip("populate with the value the will be maximum of transparency for opened dialogbox")]
    #endregion
    [Range(0, 1)]
    [SerializeField] float _dialogBoxAlpha—hannelForShow;
    #region ToolTip
    [Tooltip("Populate with acceleration  value of showing\\hiding dialogbox")]
    #endregion
    [SerializeField] float _dialogBoxSpeed;
    private Canvas _dialogBox;
    private TextMeshProUGUI _dialogBoxText;
    private Image _dialogBoxSprite;
    private Coroutine _showDialogBoxRoutine;
    private Coroutine _fadeDialogBoxRoutine;

    #region
    [Space(5)]
    [Header("Dialog Text Settings")]
    #endregion
    #region Tooltip
    [Tooltip("populate with latters adding speed value ")]
    #endregion
    [Range(0,0.3f)]
    [SerializeField] float _lettersAddingToTextSpeed;
    private Coroutine _mainDialogueRoutine;
    private Coroutine _addingLetterRoutine;



    [SerializeField] List<Dialogue> allDialogues = new List<Dialogue>();
    [SerializeField] List<Dialogue> nothingToSayDialogues= new List<Dialogue>();
    List<Dialogue> openDialogues = new List<Dialogue>();
   

   
    private WaitForFixedUpdate _waitForFixedUpdate;

    private int _diaIndex=0;
    private int _replicIndex = 0;
    private bool _isReadyToTalk = false;
    private bool _isDialogueOnGoing;
    private bool _isReadyToNextReplic=false;
    

    protected virtual void Awake()
    {
        Initialise();
        //ShowDialogBox();
    }

    private void Update()
    {
       
       
        if (_isReadyToTalk)
        {
            bool isButtonPressed = Input.GetKeyDown(Settings.KeyCode_Use);
            if (isButtonPressed)
            {
                if (!_isReadyToNextReplic && _isDialogueOnGoing)
                    _isReadyToNextReplic = true;
                

                if (allDialogues.Count==0|| _isDialogueOnGoing ==true) return;

                ShowDialogBox();
               
                if (openDialogues.Count> _diaIndex)
                    StartTheDialogue(openDialogues[_diaIndex], _replicIndex);

                else
                {
                    string replic= nothingToSayDialogues[0].replics[UnityEngine.Random.Range(0, nothingToSayDialogues[0].replics.Count)];
                    AnswerNothingToSay(replic);
                }
                                            
                
            }
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            OnZoneEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            OnZoneExit();
    }

    private void Initialise()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();
        _dialogBox = GetComponentInChildren<Canvas>();
        _dialogBoxText = GetComponentInChildren<TextMeshProUGUI>();
        _dialogBoxSprite = GetComponentInChildren<Image>();
        _dialogBox.gameObject.SetActive(false);


        for (int i = 0; i < allDialogues.Count; i++)
        {
            if (allDialogues[i].isOpen)
            {
                openDialogues.Add(allDialogues[i]);
            }
            
        }
        


        _fadeDialogBoxRoutine = StartCoroutine(FadeDialogBoxRoutine());

        UnlockNewDialogue(1);
    }

    private void ShowDialogBox()
    {
        
         _showDialogBoxRoutine = StartCoroutine(ShowDialogBoxRoutine());
       
    }

    private IEnumerator ShowDialogBoxRoutine()
    {
        float currentAlpha—hannel=0f;
        _dialogBox.gameObject.SetActive(true);
        while (currentAlpha—hannel<_dialogBoxAlpha—hannelForShow)
        {
            currentAlpha—hannel += Time.deltaTime * _dialogBoxSpeed  ;
            _dialogBoxSprite.color = new Color(256, 256, 256, currentAlpha—hannel);
           yield return _waitForFixedUpdate;
        }
        yield return _waitForFixedUpdate;
    }

    private void FadeDialogBox()
    {
        _isDialogueOnGoing = false ;
        _isReadyToNextReplic = false;
        if (_mainDialogueRoutine != null)
            StopCoroutine(_mainDialogueRoutine);
        if (_addingLetterRoutine != null)
            StopCoroutine(_addingLetterRoutine);    
        _fadeDialogBoxRoutine = StartCoroutine(FadeDialogBoxRoutine());
      
        
    }

    private IEnumerator FadeDialogBoxRoutine()
    {
        float currentAlpha—hannel = _dialogBoxAlpha—hannelForShow;
        
        while (currentAlpha—hannel > 0)
        {
            currentAlpha—hannel -= Time.deltaTime * _dialogBoxSpeed;
            _dialogBoxSprite.color = new Color(256, 256, 256, currentAlpha—hannel);
            yield return _waitForFixedUpdate;
        }
        yield return _waitForFixedUpdate;
        _dialogBox.gameObject.SetActive(false);
    }

    private void AnswerNothingToSay(string replic)
    {
        _isDialogueOnGoing = true;
        _addingLetterRoutine = StartCoroutine(AddTextRoutine(replic));
    }
    private void StartTheDialogue(Dialogue dialogue, int replicIndex)
    {
        _isDialogueOnGoing = true;
        _mainDialogueRoutine = StartCoroutine(MainDialogueRoutine(dialogue));


    }

    private IEnumerator MainDialogueRoutine(Dialogue dialogue)
    {
        for (int i = _replicIndex; i < dialogue.replics.Count; i++ )
        {
            string replic = dialogue.replics[_replicIndex];
            yield return _addingLetterRoutine= StartCoroutine(AddTextRoutine(replic));
            yield return new WaitForEndOfFrame();

            while (!_isReadyToNextReplic)
                yield return new WaitForEndOfFrame();

            _isReadyToNextReplic = false;




            if (_replicIndex == dialogue.replics.Count - 1)
            {
                EndDialog();
                break ;
            }
            _replicIndex++;

        }
       
    }

    private void EndDialog()
    {
        TakeQuest();
        _replicIndex = 0;
        _diaIndex++;
        FadeDialogBox();
    }

    private void TakeQuest()
    {
        
    }

    private IEnumerator AddTextRoutine(string replic)
    {
        _dialogBoxText.text = "";
        for (int i = 0; i < replic.Length; i++)
        {
            _dialogBoxText.text += replic[i];
            yield return new WaitForSeconds(_lettersAddingToTextSpeed);
        }
       
    }

    private void OnZoneEnter()
    {
        _isReadyToTalk = true;
        ShowDialogBox();
    }

    private void OnZoneExit()
    {
        _isReadyToTalk = false;
        FadeDialogBox();
        
       
    }

    

    public void UnlockNewDialogue(int index)
    {
        
        allDialogues[index].UnlockTheDialog() ;
        openDialogues.Add(allDialogues[index]);
    }
}

[System.Serializable]
struct Dialogue
{
    public List<string> replics;
    public bool isOpen;

    public void UnlockTheDialog()
    {
        isOpen = true;

    }
}


