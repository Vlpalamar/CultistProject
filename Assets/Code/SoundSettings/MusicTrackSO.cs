using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MusicTrack_", menuName = "Scriptable Objects/Sounds/MusicTrack")]
public class MusicTrackSO : ScriptableObject
{
    #region Header Music Track Details
    [Space(10)]
    [Header("Music Track Details")]
    #endregion
    #region Tooltip
    [Tooltip("The name  for the music track")]
    #endregion
    [SerializeField] private string musicName;
    #region Tooltip
    [Tooltip("The audio clip for the music track")]
    #endregion
    [SerializeField] private AudioClip musicClip;
    #region Tooltip
    [Tooltip("The Volume clip for the music track")]
    #endregion
    [Range(0,1)]
    [SerializeField] private float musicVolume= 0.03f;

    public string MusicName { get => musicName;  }
    public AudioClip MusicClip { get => musicClip;  }
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
}
