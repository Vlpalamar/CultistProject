using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : SingletonMonoBehaviour<UI>
{
    #region Pause
    [Space(5)]
    [Header("Pause")]
    #endregion
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] Slider hpBar;

    public Slider HpBar { get => hpBar;  }

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

    
}
