using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]

[DisallowMultipleComponent]
public class PlayerStamina : MonoBehaviour
{
    #region Details
    [Space(5)]
    [Header("Details")]
    #endregion
    [SerializeField] private int _staminaAmount=100;
    [SerializeField] private float _penaltyTime = 3f;
    [SerializeField] private float _staminaRecoveryMultiplier=10f;
    [SerializeField] private float _secondsUntilExosted = 0.5f;
    [SerializeField] private float _staminaExhastedMultiplier = 30f;


    #region Sliders
    [Space(5)]
    [Header("Sliders")]
    #endregion
    private Slider staminaBar;
    private Slider exhastedStaminaBar;

    private float _currentStamina;
    private bool _isReady = true;

    private Player _player;

    private void Start()
    {
        _currentStamina = _staminaAmount - 1f;
        _player = GameManager.Instance.GetPlayer();
        staminaBar = _player.UI.StaminaBar;
        exhastedStaminaBar = _player.UI.ExhastedStaminaBar;
        exhastedStaminaBar.value = 100;
        StartCoroutine(StaminaRecoveryRoutine());
    }

    private IEnumerator StaminaRecoveryRoutine()
    {
        do
        {
            if (_currentStamina < _staminaAmount)
            {
                _currentStamina += Time.deltaTime * _staminaRecoveryMultiplier;
                yield return new WaitForEndOfFrame();
                staminaBar.value = _currentStamina;
            }
            yield return new WaitForEndOfFrame();

        } while (true);
        
    }

    public bool TryToUse(int staminaCost)
    {
        if (!_isReady) return false;
        StartCoroutine(ShowStaminaCost());
        if (_currentStamina- staminaCost<=0)
        {
            _isReady = false;
            StartCoroutine(PenaltyTimeRoutine());
            _currentStamina = 0;
        }
        else
        {
            _currentStamina = _currentStamina - staminaCost;
        }
       
        return true;
        
    }

    private IEnumerator ShowStaminaCost()
    {
        if (exhastedStaminaBar.value<_currentStamina)
            exhastedStaminaBar.value = _currentStamina;
        yield return new WaitForSeconds(_secondsUntilExosted);
        do
        {
            exhastedStaminaBar.value -= Time.deltaTime * _staminaExhastedMultiplier;
            yield return new WaitForEndOfFrame();

        } while (exhastedStaminaBar.value>_currentStamina);
        
    }

    private IEnumerator PenaltyTimeRoutine()
    {
        yield return new WaitForSeconds(_penaltyTime);
        _isReady = true;
    }
}
