using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

[DisallowMultipleComponent]
public class CurrentWeapon : MonoBehaviour
{
    Weapon weapon;
    [SerializeField] Vector2 vectorA, vectorB;

    private Player _player;

    public Weapon Weapon { get => weapon; set => weapon = value; }

    private void Start()
    {
        _player = GameManager.Instance.GetPlayer();
    }
    private void Update()
    {
         
        if (weapon==null|| weapon.IsReady) return;
        


        weapon.CurrentTimeTillAttack -= Time.deltaTime;
        if (weapon.CurrentTimeTillAttack <= 0) weapon.IsReady = true;
    }

    public void Use(AimDirection aimDirection)
    {
        if (!weapon.IsReady == true) return;  
        if (!_player.PlayerStamina.TryToUse(weapon.WeaponDetails.StaminaCost)) return;
        

        

        //запуск анимации 
        Weapon.Use( aimDirection);
        return;
        
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        
        _player.AnimatePlayer.ChangeWeapon();
      
    }


    Player player;
    private float _offset = 1.5f;
    private float _radius = 1f;
    private void OnDrawGizmos()
    {
        //if (player == null)
        //    player = GameManager.Instance.GetPlayer();

        //Vector2 vectorPointCenter = new Vector2();

        //PlayerControl playerControll = player.GetComponent<PlayerControl>();

        //switch (playerControll._aimDirection)
        //{
        //    case AimDirection.top:
               
        //        vectorPointCenter = new Vector2(player.transform.position.x, player.transform.position.y + _offset);
        //        break;

        //    case AimDirection.right:
              
        //        vectorPointCenter = new Vector2(player.transform.position.x + _offset, player.transform.position.y);
        //        break;

        //    case AimDirection.down:
                
        //        vectorPointCenter = new Vector2(player.transform.position.x, player.transform.position.y - _offset);
        //        break;

        //    case AimDirection.left:
               
        //        vectorPointCenter = new Vector2(player.transform.position.x - _offset, player.transform.position.y);
        //        break;

        //    default:
        //        break;
        //}

        //Gizmos.DrawSphere(vectorPointCenter, 1.5f);
    }
}
