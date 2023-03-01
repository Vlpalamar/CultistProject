using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private MusicTrackSO mainMenuTheme;

    private void Start()
    {
        MusicManager.Instance.PlayMusic(mainMenuTheme,0.2f, 2f);
        
    }
}
