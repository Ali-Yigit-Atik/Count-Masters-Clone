using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Dotween kütüphansei

public class obstacles : MonoBehaviour
{
    public bool isRightHammer = false;

    private void Start()
    {
        if (gameObject.CompareTag("hammer"))
        {

            if (isRightHammer)
            {
                transform.DORotate(new Vector3(0, 0, 90), 1.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }

            else if (isRightHammer == false)
            {
                transform.DORotate(new Vector3(0, 0, -90), 1.5f).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
            }
        }

        if(gameObject.CompareTag("obstacle") && gameObject.transform.parent.gameObject.CompareTag("sharp"))
        {
            transform.DOMoveY(-2.2f, Random.Range(0.2f, 2f)).SetLoops(10000, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        
        

        if (gameObject.CompareTag("obstacle"))
        {
            if (collision.gameObject.CompareTag("teamMember"))
            {
                               
                collision.gameObject.tag = "Untagged";

                for (int i = 0; i < newMemberSpawn.members.Count; i++)
                {
                    if (newMemberSpawn.members[i].name == collision.gameObject.name)
                    {
                        newMemberSpawn.members.RemoveAt(i);
                        collision.gameObject.SetActive(false);
                        break;
                    }
                }
                              

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("parent obstacle"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {

                order.isNeedOrder = false;


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.CompareTag("parent obstacle"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {


                StartCoroutine("needOrder");

            }
        }
    }

    IEnumerator needOrder()
    {
        yield return new WaitForSeconds(0.5f);
        order.isNeedOrder = true;
        Debug.Log("düzgün çalýþýyor");
    }

}
