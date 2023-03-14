using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveToPositionEvent))]
[RequireComponent(typeof(MoveToPosition))]
[RequireComponent(typeof(EnemyMovementAI))]
[RequireComponent(typeof(EnemyEffects))]

[DisallowMultipleComponent]
[System.Serializable]
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
    private Player player ;
    private EnemyEffects enemyEffects;
    private bool isAlive=true;

    public EnemyDetailsSO EnemyDetails { get => enemyDetails; }
    public MoveToPositionEvent MoveToPosition { get => moveToPosition;  }
    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public Player _Player { get => player;  }
    public EnemyEffects EnemyEffects { get => enemyEffects;  }

    private void Awake()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        moveToPosition = GetComponent<MoveToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        _boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.GetPlayer();
        enemyEffects = GetComponent<EnemyEffects>();
    }
}
