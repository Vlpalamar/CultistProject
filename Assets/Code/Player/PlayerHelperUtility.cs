using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelperUtility : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }

    public void MoveFromPointToPoint(Vector2 currentPosition,  float speed, int distance)
    {

        Vector3 mouseWorldPoint = HelperUtilities.GetMouseWorldPosition();
        Vector2 direction = (mouseWorldPoint - player.transform.position).normalized;

       // Vector2 endPoint = player.Rigidbody.position + (direction * distance * Time.deltaTime);

        StartCoroutine(MoveInMouseDirectionRoutine(distance, player.Rigidbody, direction, speed));
    }


    private IEnumerator MoveInMouseDirectionRoutine(int distance, Rigidbody2D playerRigigBody,Vector2 direction, float speed)
    {

       
        while (distance > 0 )
        {
            distance--;
            playerRigigBody.MovePosition(playerRigigBody.position + (direction*Time.deltaTime*speed));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

    }

}
