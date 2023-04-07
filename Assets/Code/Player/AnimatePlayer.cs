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

    [SerializeField] private SkeletonAnimation skelletonLeftStillet;
    [SerializeField] private SkeletonAnimation skelletonRightStillet;
    [SerializeField] private SkeletonAnimation skelletonFrontStillet;
    [SerializeField] private SkeletonAnimation skelletonDownStillet;

    [SerializeField] private SoundEffectSO stepSounEffect;

    private string state;
    private string currentAnimationName="";
    private AimDirection currentDirection;

    #region animations names
    private string animationWalk = "Run";
    private string animationIdle= "Idle";
    private string animationAttack = "Attack";

    #endregion
    bool _isPause=false;
    bool isIdle = false;
    bool _isReadyToChange=true;

    private Player player;
    private SkeletonAnimation animator;

    public string AnimationWalk { get => animationWalk; set => animationWalk = value; }
    public string AnimationAttack { get => animationAttack; set => animationAttack = value; }

    private void Awake()
    {
        player = GetComponent<Player>(); 

        

        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        //Spine.AnimationState state = skeletonAnimation.AnimationState;
        //state.Event += OnEvents;
        //state.Complete += Complete;
        DisableAllSkeletons();
    }

    
   

    private void OnEnable()
    {

        player.MovementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocyty;
        player.AimWeaponEvent.OnWeaponAim += AimWeaponEvent_OnWeaponAim;
        player.IdleEvent.OnIdle += IdleEvent_OnIdle;
    }



    private void OnDisable()
    {
        player.MovementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocyty;
        player.AimWeaponEvent.OnWeaponAim -= AimWeaponEvent_OnWeaponAim;
        player.IdleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void IdleEvent_OnIdle(IdleEvent obj)
    {
        SetAnimation(currentDirection, animationIdle, true, 1);
    
    }

    private void Complete(TrackEntry trackEntry)
    {

    }

    private void AimWeaponEvent_OnWeaponAim(AimWeaponEvent aimWeaponEvent, AimWeaponEventArgs args)
    {
        InitializeAimAnimationParameters();
        SetAnimationParameters(args.AimDirection);
    }

    private void MovementByVelocityEvent_OnMovementByVelocyty(MovementByVelocityEvent arg1, MovementByVelocityArgs arg2)
    {
        SetAnimation(currentDirection,animationWalk, true, 1);
    }

    private void SetAnimationParameters(AimDirection aimDirection)
    {
        if (!_isReadyToChange) return;
       

        
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

    public void SetAnimation(AimDirection aimDirection,string animation, bool loop, float timeScale=1f)
    {
        if (!_isReadyToChange) return;   
        if (_isPause) return;
        if (animator == null) return;
        
        if (animation== currentAnimationName&&aimDirection== currentDirection)
            return;

        Spine.TrackEntry trackEntry= animator.state.SetAnimation(0, animation, loop);
        trackEntry.TimeScale = timeScale;
        trackEntry.Event += OnEvents;

        currentAnimationName = animation;
        currentDirection = aimDirection;
    }


    public void SetAttackAnimation(AimDirection aimDirection, string animation, bool loop, float timeScale=1f)
    {
        if (!_isReadyToChange) return;
        
        if (animation == currentAnimationName && aimDirection == currentDirection )
            return;
        _isReadyToChange = false;

        Spine.TrackEntry trackEntry = animator.state.SetAnimation(0, animation, loop);
        trackEntry.TimeScale = timeScale;
        trackEntry.Event += OnEvents;
        trackEntry.Complete += OnAttackEnd;


        currentAnimationName = animation;
        currentDirection = aimDirection;

    }

    private void OnAttackEnd(TrackEntry trackEntry)
    {
        _isReadyToChange = true;
       
    }

    private void OnEvents(TrackEntry trackEntry, Spine.Event e)
    {
        print(e.Data.Name);
       
        if (e.Data.Name=="run")
            SoundEffectManager.Instance.PlaySoundEffect(stepSounEffect);
        if (e.Data.Name == "attack")
            player.Weapon.Use(currentDirection);
        if (e.Data.Name == "attak")
            player.Weapon.Use(currentDirection);




    }

    private void InitializeAimAnimationParameters()
    {

    }

    public void ChangeWeapon()
    {

        _isPause = true;
        animator.AnimationName = "";
        skelletonDown.gameObject.SetActive(false) ;
        skelletonFront.gameObject.SetActive(false); ;
        skelletonLeft.gameObject.SetActive(false); ;
        skelletonRight.gameObject.SetActive(false); ;
     
        

        skelletonDown = skelletonDownStillet;
        skelletonFront = skelletonFrontStillet;
        skelletonLeft = skelletonLeftStillet;
        skelletonRight = skelletonRightStillet;
        animationWalk = "Run";
        animationIdle = "Idle";

        Invoke(nameof(UnPouse), 0.1f);

    }

    private void UnPouse()
    {
        _isPause = false;
    }
}