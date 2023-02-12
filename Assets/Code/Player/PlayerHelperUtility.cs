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

    public void MoveFromPointToPoint(Vector2 currentPosition,  float speed, float distance)
    {

        Vector3 mouseWorldPoint = HelperUtilities.GetMouseWorldPosition();
        Vector2 direction = (mouseWorldPoint - player.transform.position).normalized;

        Vector2 endPoint = player.Rigidbody.position + (direction * distance * Time.deltaTime);

        StartCoroutine(MoveInMouseDirectionRoutine(endPoint, player.Rigidbody, direction, speed));
    }


    private IEnumerator MoveInMouseDirectionRoutine(Vector2 endPoint, Rigidbody2D playerRigigBody,Vector2 direction, float speed)
    {

        float minDist = 0.2f;
        float i = 150;
        while (Vector3.Distance(player.transform.position, endPoint) > minDist && i >0 )
        {
            i--;
            playerRigigBody.MovePosition(playerRigigBody.position + (direction*Time.deltaTime*speed));
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

    }

}
