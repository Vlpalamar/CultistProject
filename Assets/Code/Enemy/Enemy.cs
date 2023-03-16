using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveToPositionEvent))]
[RequireComponent(typeof(MoveToPosition))]
[RequireComponent(typeof(EnemyMovementAI))]
[RequireComponent(typeof(EnemyEffects))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Loot))]

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
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody;
    private Player player ;
    private EnemyEffects enemyEffects;
    private EnemyAnimation enemyAnimation;
    private EnemyAttack enemyAttack;
    private Loot loot;
    private bool isAlive=true;

    public EnemyDetailsSO EnemyDetails { get => enemyDetails; }
    public MoveToPositionEvent MoveToPosition { get => moveToPosition;  }
    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public Player _Player { get => player;  }
    public EnemyEffects EnemyEffects { get => enemyEffects;  }
    public EnemyAnimation EnemyAnimation { get => enemyAnimation;  }
    public EnemyAttack EnemyAttack { get => enemyAttack;  }
    public EnemyMovementAI EnemyMovementAI { get => enemyMovementAI; }
    public BoxCollider2D BoxCollider { get => boxCollider; }
    public Loot Loot { get => loot; }

    private void Awake()
    {
        enemyMovementAI = GetComponent<EnemyMovementAI>();
        moveToPosition = GetComponent<MoveToPositionEvent>();
        idleEvent = GetComponent<IdleEvent>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.GetPlayer();
        enemyEffects = GetComponent<EnemyEffects>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyAttack = GetComponent<EnemyAttack>();
        loot = GetComponent<Loot>();
    }
   
}
