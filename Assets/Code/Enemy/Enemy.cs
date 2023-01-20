using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveToPositionEvent))]
[RequireComponent(typeof(MoveToPosition))]
[RequireComponent(typeof(EnemyMovementAI))]
[DisallowMultipleComponent]
public  class Enemy : MonoBehaviour
{
    #region ToolTip

    #endregion
    [SerializeField] private EnemyDetailsSO enemyDetails;

    private EnemyMovementAI enemyMovementAI;
    private IdleEvent idleEvent;
    private MoveToPositionEvent moveToPosition;
    private BoxCollider2D _boxCollider;
    private Rigidbody2D rigidbody;

    public EnemyDetailsSO EnemyDetails { get => enemyDetails; }
    public MoveToPositionEvent MoveToPosition { get => moveToPosition;  }
    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }

    private void Awake()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        moveToPosition = GetComponent<MoveToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        _boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
