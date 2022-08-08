using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public static bool dontMoveLeft=false;
    public static bool dontMoveRight = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("teamMember"))
        {

            if (gameObject.CompareTag("leftWall"))
            {
                dontMoveLeft = true;
                Debug.Log("left wall touched");
            }

            if (gameObject.CompareTag("rightWall"))
            {
                dontMoveRight = true;
                Debug.Log("right wall touched");
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        dontMoveLeft = false;
        dontMoveRight = false;
    }
}
