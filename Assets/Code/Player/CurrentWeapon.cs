using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    Weapon weapon;
    [SerializeField] Vector2 vectorA, vectorB;

    public Weapon Weapon { get => weapon; set => weapon = value; }

    private void Start()
    {
        
    }
    private void Update()
    {
         
        if (weapon==null|| weapon.IsReady) return;
        


        weapon.CurrentTimeTillAttack -= Time.deltaTime;
        if (weapon.CurrentTimeTillAttack <= 0) weapon.IsReady = true;
    }

    public void Use(AimDirection aimDirection)
    {

        //запуск анимации 
        Weapon.Use( aimDirection);
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
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
