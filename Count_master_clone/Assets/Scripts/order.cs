using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class order : MonoBehaviour
{
    public static bool isNeedOrder = true;
    private void Update()
    {

        if (gameObject.CompareTag("soldier"))
        {

            gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.8f + 1.5f / (enemy.enemies.Count)));
            //isNeedOrder = true;
        }

        //if (PlayerController.isbattle == true)
        //{
        //    //if (gameObject.CompareTag("teamMember")) {
        //    //
        //    //    gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.8f+1.5f/(newMemberSpawn.members.Count)));
        //    //    //isNeedOrder = true;
        //    //}
        //
        //    //if (gameObject.CompareTag("soldier"))
        //    //{
        //    //
        //    //    gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.8f + 1.5f / (enemy.enemies.Count)));
        //    //    //isNeedOrder = true;
        //    //}
        //    //isNeedOrder = true;
        //}

        if (isNeedOrder == true)
        {
            //if (gameObject.CompareTag("Player"))
            //{
            //
            //    foreach (var x in newMemberSpawn.members)
            //    {
            //        
            //        x.transform.localPosition = Vector3.MoveTowards(x.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 1f);
            //        Debug.Log("order çalýþýyor");
            //    
            //    }
            //    isNeedOrder = false;
            //}


            //if (gameObject.CompareTag("teamMember"))
            //{
            //
            //    gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 1), Time.deltaTime * 2f);
            //    //isNeedOrder = true;
            //}


            //StartCoroutine("order_");

            if (gameObject.CompareTag("teamMember"))
            {

                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.5f + 1.5f / (newMemberSpawn.members.Count)));
                //isNeedOrder = true;
            }

        }

        //if (gameObject.CompareTag("teamMember"))
        //{
        //
        //    gameObject.transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 0, 1), Time.deltaTime * 1f);
        //    //isNeedOrder = true;
        //}


    }

    IEnumerator order_()
    {
        
        yield return new WaitForSeconds(3f);
        isNeedOrder = false;
        Debug.Log("order çalýþýyor");
    }
}
