using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MeleeWeapon
{


    private float _offset = 1f;
    private float _radius = 1.5f;
    private Transform player;

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
                vectorPointCenter = new Vector2(player.position.x, player.position.y + _offset);
                break;
            
            case AimDirection.right:
                print("right");
                vectorPointCenter = new Vector2(player.position.x + _offset, player.position.y );
                break;
            
            case AimDirection.down:
                print("down");
                vectorPointCenter = new Vector2(player.position.x, player.position.y - _offset);
                break;

            case AimDirection.left:
                print("left");
                vectorPointCenter = new Vector2(player.position.x - _offset, player.position.y );
                break;

            default:
                break;
        }
        print(vectorPointCenter);
        Collider2D[] hits = Physics2D.OverlapCircleAll(vectorPointCenter, _radius);
        HitAllInArrea(hits);


        
    }

    private void Initialise()
    {
        player = GameManager.Instance.GetPlayer().transform;
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
