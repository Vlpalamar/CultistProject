using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MeleeWeapon
{
    [SerializeField] private float _pushSpeed;
    [SerializeField] private float _pushDistance;

    private float _offset = 1f;
    private float _radius = 1.5f;
    private Player player;

    public Dagger(WeaponDetailsSO weaponDetailsSO) : base(weaponDetailsSO)
    {
    }

    public override void Use(AimDirection aimDirection)
    {
        
        if (IsReady)
        {
            MakeHitArrea(aimDirection);
            this.CurrentTimeTillAttack = this.WeaponDetails.Delay;
            IsReady = false;
        }
      
    }

    private void MakeHitArrea(AimDirection aimDirection)
    {

        if (player == null)
        {
            Initialise();
            
        }
            
        
        Vector2 vectorPointCenter = new Vector2();

        
      
        switch (aimDirection)
        {
            case AimDirection.top:
                print("top");
                vectorPointCenter = new Vector2(player.transform.position.x, player.transform.position.y + _offset);
                break;
            
            case AimDirection.right:
                print("right");
                vectorPointCenter = new Vector2(player.transform.position.x + _offset, player.transform.position.y );
                break;
            
            case AimDirection.down:
                print("down");
                vectorPointCenter = new Vector2(player.transform.position.x, player.transform.position.y - _offset);
                break;

            case AimDirection.left:
                print("left");
                vectorPointCenter = new Vector2(player.transform.position.x - _offset, player.transform.position.y );
                break;

            default:
                break;
        }
        print(vectorPointCenter);
        Collider2D[] hits = Physics2D.OverlapCircleAll(vectorPointCenter, _radius);
        HitAllInArrea(hits);

        MoveInMouseDirection();

        
    }

    private void MoveInMouseDirection()
    {
       
       // player.Rigidbody.MovePosition(endPoint);

        player.PlayerHelperUtility.MoveFromPointToPoint(player.transform.position,  _pushSpeed, _pushDistance);

    }

   
    private void Initialise()
    {
        player = GameManager.Instance.GetPlayer();
        _offset = 1.5f;
        _radius = 1.5f;
    }

    private void HitAllInArrea(Collider2D[] hits)
    {
          
       
        if (hits == null) return;
        foreach (Collider2D hit in hits)
        {
           
            Health hitHealth = hit.GetComponent<Health>();
            if (hitHealth == null||hit.GetComponent<Transform>()==player) continue;

            print(hit.name);
            hitHealth.GetDamage(this.weaponDetails.Damage);
            print("!");
        }
    }

   
}
