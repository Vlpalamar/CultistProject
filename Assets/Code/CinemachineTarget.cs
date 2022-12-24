using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachineTarget : MonoBehaviour
{
    private  CinemachineTargetGroup _cinemachineTarget;
    #region Tooltip
    [Tooltip("Populate with the CursorTarget gameObject")]
    #endregion
    [SerializeField] private Transform _cursorTarget;

    private void Awake()
    {
        _cinemachineTarget = GetComponent<CinemachineTargetGroup>();
    }

    private void Start()
    {
        SetCinemachineTargetGroup();
    }



    private void SetCinemachineTargetGroup()
    {
        //print(GameManager.Instance.GetPlayer() + "  вафіва !");
        CinemachineTargetGroup.Target cinemachineTarget_player = new CinemachineTargetGroup.Target 
        { weight = 1f, radius = 2.5f, target = GameManager.Instance.GetPlayer().transform };
       

        CinemachineTargetGroup.Target cinemachineTarget_cursor = new CinemachineTargetGroup.Target
        { weight = 1f, radius = 1f, target = _cursorTarget };

        CinemachineTargetGroup.Target[] cinemachineTargets = new CinemachineTargetGroup.Target[] { cinemachineTarget_player, cinemachineTarget_cursor };
        _cinemachineTarget.m_Targets = cinemachineTargets;
    }

    private void Update()
    {
        _cursorTarget.position = AimHelperUtilities.GetMouseWorldPosition();
    }

}
