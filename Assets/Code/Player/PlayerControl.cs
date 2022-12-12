using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerControl : MonoBehaviour
{

    private Player player;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MovementInput();
    }
    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

        if (horizontalMovement != 0f && verticalMovement != 0f)
            direction *= 0.7f;
        

        if(direction !=Vector2.zero)
        {
            player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, moveSpeed);
        }
        else
        {
            //idle
            player.MovementByVelocityEvent.CallMovementByVelocityEvent(direction, 0);
        }


    }
}
