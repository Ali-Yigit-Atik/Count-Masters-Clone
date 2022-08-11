using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Dotween kütüphansei

public class obstacles : MonoBehaviour
{
    public bool isRightHammer = false;
    private GameObject boss;
    private int returnTime = 0;
    private float firstHealthBoss;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("boss");
        firstHealthBoss = boss.GetComponent<bossBattle>().bossHealth;


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

        if (gameObject.CompareTag("obstacle") && gameObject.transform.parent.gameObject.CompareTag("sharp"))
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

        if (gameObject.CompareTag("bossHitArea"))
        {


            if (other.gameObject.CompareTag("teamMember") && boss.GetComponent<bossBattle>().bossHealth >= 0)
            {

                //if (bossBattle.bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1 && bossBattle.bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5)
                //{
                //    other.gameObject.tag = "Untagged";
                //
                //    for (int i = 0; i < newMemberSpawn.members.Count; i++)
                //    {
                //        if (newMemberSpawn.members[i].name == other.gameObject.name)
                //        {
                //            newMemberSpawn.members.RemoveAt(i);
                //            other.gameObject.SetActive(false);
                //            break;
                //        }
                //    }
                //    
                //}


                other.gameObject.tag = "Untagged";

                for (int i = 0; i < newMemberSpawn.members.Count; i++)
                {
                    if (newMemberSpawn.members[i].name == other.gameObject.name)
                    {
                        //newMemberSpawn.members.RemoveAt(i);
                        //other.gameObject.SetActive(false);
                        StartCoroutine(deathTime(i, other.gameObject));
                        break;
                    }
                }
            }

            if (boss.GetComponent<bossBattle>().bossHealth <= 0)
            {
                other.gameObject.GetComponent<Animator>().SetFloat("attack", 2);

                var membersRotation2 = new Vector3(Camera.main.transform.position.x, other.gameObject.transform.position.y, Camera.main.transform.position.z) - other.gameObject.transform.position;
                other.gameObject.transform.rotation = Quaternion.Slerp(other.gameObject.transform.rotation, Quaternion.LookRotation(membersRotation2, Vector3.up), 10f * Time.deltaTime);

                
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
        yield return new WaitForSeconds(1f);
        order.isNeedOrder = true;
        Debug.Log("düzgün çalýþýyor");
    }


    IEnumerator deathTime(int i, GameObject a)
    {
        //if (bossBattle.bossAnimator.GetInteger("attackMode") == )
        //{
        //
        //}

        //if (boss.GetComponent<bossBattle>().bossHealth <= 0)
        //{
        //    a.gameObject.GetComponent<Animator>().SetFloat("attack", 2);
        //}



        yield return new WaitForSeconds(1.5f);

         if (boss.GetComponent<bossBattle>().bossHealth > 0)
         {
             boss.GetComponent<bossBattle>().bossHealth--;


            

             yield return new WaitForSeconds(0.3f);


             newMemberSpawn.members.RemoveAt(i);
             a.gameObject.SetActive(false);
         }


        if (boss.GetComponent<bossBattle>().bossHealth <= 0)
        {
            a.tag = "teamMember";            
        
        }




    }
}
