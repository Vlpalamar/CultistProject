using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyMovementAI : MonoBehaviour
{
    #region Tooltip
    [Tooltip("MovementDetails scriptable object containing movement details such as speed")]
    #endregion
    [SerializeField] private MovementDetailsSO movementDetails;
    private Enemy _enemy;
    private Player _player;
    private Vector2 _playerPosition;
    private Queue<Vector2Int> _movementSteps;
    private float _currentEnemyPathRebuildColdown = 2f;
    private Coroutine _moveEnemyRoutine;
    private WaitForFixedUpdate _waitForFixedUpdate;
    private bool _isChasingPlayer = false;

    private float moveSpeed;

    public float MoveSpeed { get => moveSpeed; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _movementSteps = new Queue<Vector2Int>();

        moveSpeed = movementDetails.MoveSpeed;
    }

    private void Start()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();

        _player = GameManager.Instance.GetPlayer();
        _playerPosition = _player.transform.position;


    }
    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        _currentEnemyPathRebuildColdown -= Time.deltaTime;
        if (!_isChasingPlayer && Vector3.Distance(this.transform.position, _player.transform.position) < _enemy.EnemyDetails.ChaseDistance)
            _isChasingPlayer = true;

        if (!_isChasingPlayer)
            return;

        if (_currentEnemyPathRebuildColdown <= 0f || (Vector3.Distance(_player.transform.position, _playerPosition) > Settings.playerMoveDistanceToRebuildPath))
        {
            _currentEnemyPathRebuildColdown = Settings.enemyPathRebuildCooldown;

            _playerPosition = _player.transform.position;

            CreatePath();

            if (_movementSteps != null)
            {
                if (_moveEnemyRoutine != null)
                {
                    _player.Rigidbody.velocity = new Vector2(0, 0);
                    StopCoroutine(_moveEnemyRoutine);
                }

                _moveEnemyRoutine = StartCoroutine(MoveEnemyRoutine(_movementSteps));

            }

        }




    }

    private IEnumerator MoveEnemyRoutine(Queue<Vector2Int> movementSteps)
    {
        while(movementSteps.Count>0)
        {
            Vector2 nextPosition = movementSteps.Dequeue();
            Vector3Int newVector = new Vector3Int(Mathf.RoundToInt(nextPosition.x), Mathf.RoundToInt(nextPosition.y), 0);
            Vector2 nextStep = AStar.Instance.Grid.CellToWorld(newVector);

            print("nextPosition:"+ nextStep);
            while (Vector2.Distance(nextStep, transform.position)>0.2f)
            {
                _enemy.MoveToPosition.CallOnMoveToPositionEvent(transform.position, nextStep, (nextStep - (Vector2)transform.position).normalized, moveSpeed);

                

                yield return _waitForFixedUpdate;

            }
            yield return _waitForFixedUpdate;
        }
        _player.Rigidbody.velocity = new Vector2(0,0);
    }

    private void CreatePath()
    {

        Vector2Int ennemyGridPosition =(Vector2Int) AStar.Instance.Grid.WorldToCell(this.transform.position);

        Vector2Int playerGridPosition = (Vector2Int)AStar.Instance.Grid.WorldToCell(_player.transform.position);

        Vector2Int playerPos = GetNearestNonObstaclePlayerPosition(playerGridPosition);

        _movementSteps = AStar.Instance.BuidlPath(ennemyGridPosition, playerGridPosition);

        if (_movementSteps != null)
           _movementSteps.Dequeue();
        
        else
            _player.Rigidbody.velocity = new Vector2(0, 0);
        
    }



    private Vector2Int GetNearestNonObstaclePlayerPosition(Vector2Int playerGridPosition)
    {
        Vector3Int playerPosition = (Vector3Int) playerGridPosition;
        Vector3Int playerCellPosition = AStar.Instance.Grid.WorldToCell(playerPosition);

        if (AStar.Instance.AStarMovementPenaltyDictionary.Contains(new Vector2Int(playerCellPosition.x, playerCellPosition.y)))
        {
            return (Vector2Int) playerCellPosition;
        }
        else
        {
            for (int i = -1; i <=1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j == 0 && i == 0)
                        continue;
                    try
                    {
                        if (!AStar.Instance.AStarMovementPenaltyDictionary.Contains(new Vector2Int(playerCellPosition.x + i, playerCellPosition.y + j)))
                            return new Vector2Int(playerCellPosition.x + i, playerCellPosition.y + j);
                    }
                    catch (Exception)
                    {

                        continue;
                    }

                }
                          
            }
        }
        return (Vector2Int)playerCellPosition;

    }
}
