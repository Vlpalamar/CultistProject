using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skelletonLeft;
    [SerializeField] private SkeletonAnimation skelletonRight;
    [SerializeField] private SkeletonAnimation skelletonFront;
    [SerializeField] private SkeletonAnimation skelletonDown;



    private string currentAnimationName = "";
    private AimDirection currentDirection;

    private Player _player;
    private Enemy _enemy;
    private SkeletonAnimation animator;

    private bool _isReadyToChange = true;


    #region AnimationNames
    private string animationWalk = "Run";
    private string animationAttack = "Attak";
    private string animationRush = "Blink";

    public string AnimationWalk { get => animationWalk; }
    public string AnimationAttack { get => animationAttack; }
    public string AnimationLeap { get => animationRush; }
    public AimDirection CurrentDirection { get => currentDirection; }
    #endregion


    private void Awake()
    {

        _enemy = GetComponent<Enemy>();


        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        //Spine.AnimationState state = skeletonAnimation.AnimationState;
        //state.Event += OnEvents;
        //state.Complete += Complete;

    }
    private void Start()
    {

        DisableAllSkeletons();
        _player = GameManager.Instance.GetPlayer();

    }

    private void Update()
    {
        if (!_isReadyToChange) return;

        GetAim();
        SetAnimationParameters(CurrentDirection);

    }


    public void SetAttackAnimation(AimDirection aimDirection, string animation, bool loop, float timeScale)
    {

        if (animation == currentAnimationName && aimDirection == CurrentDirection)
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
        _enemy.EnemyMovementAI.CountinueChasing();

    }

    private void GetAim()
    {
        Vector3 playerDirection = (_player.transform.position - transform.position);
        float playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);
        currentDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);
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


    public void SetAnimation(AimDirection aimDirection, string animation, bool loop, float timeScale)
    {
        if (animator == null) return;
        if (animation == currentAnimationName && aimDirection == CurrentDirection)
            return;





        Spine.TrackEntry trackEntry = animator.state.SetAnimation(0, animation, loop);
        trackEntry.TimeScale = timeScale;
        trackEntry.Event += OnEvents;

        currentAnimationName = animation;
        currentDirection = aimDirection;

    }

    private void OnEvents(TrackEntry trackEntry, Spine.Event e)
    {
        print(e.Data.Name);
        if (e.Data.Name.ToLower() == "attak")
        {
            _enemy.EnemyAttack.DealDamage(_enemy.EnemyAttack.AttackDetails.Damage);
        }

        

    }

    private void DisableAllSkeletons()
    {
        skelletonLeft.gameObject.SetActive(false);
        skelletonDown.gameObject.SetActive(false);
        skelletonRight.gameObject.SetActive(false);
        skelletonFront.gameObject.SetActive(false);
    }
}
