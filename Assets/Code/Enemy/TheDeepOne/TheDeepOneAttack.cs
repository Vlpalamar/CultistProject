
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDeepOneAttack : EnemyAttack
{
    
    [SerializeField] private float _deshDuration=20f;
    [SerializeField] private float _deshSpeed =15f;
    [SerializeField] private float _deshCooldown = 10f;
    [SerializeField] private float _deshDamage=100;

    private bool _isReadyForLeap = false;
    
    override protected void Start()
    {
        base.Start();
        StartCoroutine(DefineNextAttack());
    }


    private void Update()
    {
       
        if (!_isReadyForLeap)
            _enemy.EnemyMovementAI.IsEnoughRangeToAttack();
        else
            Leap();
    }

    private void Leap()
    {
        _isReadyForLeap = false;
        Vector2 direction = (_player.transform.position - this.transform.position).normalized;

        _enemy.EnemyAnimation.SetAttackAnimation(_enemy.EnemyAnimation.CurrentDirection, _enemy.EnemyAnimation.AnimationLeap, false, 1);
        _enemy.EnemyMovementAI.MovementSteps.Clear();
        StartCoroutine(LeapRoutine(_deshDuration, _enemy.Rigidbody, direction, _deshSpeed));
    }

    private IEnumerator LeapRoutine(float deshLeapDistance, Rigidbody2D rigidbody, Vector2 direction, float deshSpeed)
    {
        _enemy.EnemyMovementAI.MoveSpeed = _enemy.EnemyMovementAI.MovementDetails.MoveSpeed + _deshSpeed;
        float checkIsEnemyTouchThePlayerTimer=0.1f;
        float i = _deshDuration / checkIsEnemyTouchThePlayerTimer;

        while (i>0)
        {
            i--;
            if (Vector3.Distance(_player.transform.position, this.transform.position)<1f)
            {
                DealDamage(_deshDamage);
            }
            yield return new WaitForSeconds(checkIsEnemyTouchThePlayerTimer);
        }
       
        _enemy.EnemyMovementAI.MoveSpeed = _enemy.EnemyMovementAI.MovementDetails.MoveSpeed;

       
    }

    private IEnumerator DefineNextAttack()
    {
        int persentsForLeap=20;
        while (true)
        {
            int i = Random.Range(0, 100);

            if (i < persentsForLeap)
                _isReadyForLeap = true;
            else
                _isReadyForLeap = false;
            yield return new WaitForSeconds(_deshCooldown);
        }

    }

    public override void StartAttack()
    {
       // print("Attack");
        _enemy.EnemyAnimation.SetAttackAnimation(_enemy.EnemyAnimation.CurrentDirection, _enemy.EnemyAnimation.AnimationAttack, true , 1);
    }
    public override void DealDamage( float Damage )
    {
        if (Vector3.Distance(this.transform.position, _player.transform.position) < AttackDetails.AttackRange+0.5f)
        {
            _player.Health.GetDamage(Damage);
        }
    }

   
}
