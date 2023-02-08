using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="SoundEffect_", menuName ="Scriptable Objects/Sounds/SoundEffect")]
public class SoundEffectSO : ScriptableObject
{
    #region Header Sound Effect Details
    [Space(10)]
    [Header("Sound Effect Details")]
    #endregion
    [SerializeField] private string soundEffectsName;

    [SerializeField] private GameObject soundPrefab;

    [SerializeField] private AudioClip audioClip;

    #region Tooltip
    [Tooltip("pitch- is addition setting to Clip in order to sound higher or lower ")]
    #endregion
    [Range(0.1f, 1.5f)]
    [SerializeField] private float soundPitchRandomVariationMin = 0.8f;

    #region Tooltip
    [Tooltip("pitch- is addition setting to Clip in order to sound higher or lower ")]
    #endregion
    [Range(0.1f, 1.5f)]
    [SerializeField] private float soundPitchRandomVariationMax = 1.2f;

    [Range(0f, 1f)]
    [SerializeField] private float soundEffectVolume = 0.5f;





    public string SoundEffectsName { get => soundEffectsName;  }
    public GameObject SoundPrefab { get => soundPrefab;  }
    public AudioClip AudioClip { get => audioClip;   }
    public float SoundPitchRandomVariationMin { get => soundPitchRandomVariationMin; }
    public float SoundPitchRandomVariationMax { get => soundPitchRandomVariationMax; }
    public float SoundEffectVolume { get => soundEffectVolume;  }
}
