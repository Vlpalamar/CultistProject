using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{


    [SerializeField] private SkeletonAnimation skelletonLeft;
    [SerializeField] private SkeletonAnimation skelletonRight;
    [SerializeField] private SkeletonAnimation skelletonFront;
    [SerializeField] private SkeletonAnimation skelletonDown;

    [SerializeField] private SoundEffectSO stepSounEffect;

    private string state;
    private string currentAnimationName="";
    private AimDirection currentDirection;

    #region animations names
    const string AnimationWalk = "animation";

    #endregion


    private Player player;
    private SkeletonAnimation animator;



    private void Awake()
    {
        player = GetComponent<Player>(); 

        

        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        //Spine.AnimationState state = skeletonAnimation.AnimationState;
        //state.Event += OnEvents;
        //state.Complete += Complete;
        DisableAllSkeletons();
    }

    private void Complete(TrackEntry trackEntry)
    {
        
    }

    private void OnEnable()
    {

        player.MovementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocyty;
        player.AimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
    }



    private void OnDisable()
    {

        player.MovementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocyty;

        player.AimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs args)
    {

        InitializeAimAnimationParameters();

        SetAnimationParameters(args.AimDirection);

    }

    private void MovementByVelocityEvent_OnMovementByVelocyty(MovementByVelocityEvent arg1, MovementByVelocityArgs arg2)
    {


        
        SetAnimation(currentDirection,AnimationWalk, true, 1);

    }

    private void SetAnimationParameters(AimDirection aimDirection)
    {
        switch (aimDirection)
        { 

            case AimDirection.top:

                animator = skelletonFront;
                break;

            case AimDirection.right:
                animator = skelletonRight;
                break;

            case AimDirection.down:
               animator = skelletonDown;
                break;

            case AimDirection.left:
                animator = skelletonLeft;
                break;

               
            default:
                break;
        }

        if (animator.gameObject.activeSelf)
            return;

        currentAnimationName = "";
        DisableAllSkeletons();
        animator.gameObject.SetActive(true);

        


    }

    private void DisableAllSkeletons()
    {
        skelletonLeft.gameObject.SetActive(false);
        skelletonDown.gameObject.SetActive(false);
        skelletonRight.gameObject.SetActive(false);
        skelletonFront.gameObject.SetActive(false);
    }

    public void SetAnimation(AimDirection aimDirection,string animation, bool loop, float timeScale)
    {
        if (animator == null) return;
        if (animation== currentAnimationName&&aimDirection== currentDirection)
            return;





        Spine.TrackEntry trackEntry= animator.state.SetAnimation(0, animation, loop);
        trackEntry.TimeScale = timeScale;
        trackEntry.Event += OnEvents;

        currentAnimationName = animation;
        currentDirection = aimDirection;
    }

    private void OnEvents(TrackEntry trackEntry, Spine.Event e)
    {
        //print(e.Data.Name);
       
        if (e.Data.Name=="run1")
            SoundEffectManager.Instance.PlaySoundEffect(stepSounEffect);
        
    }

    private void InitializeAimAnimationParameters()
    {

    }

}