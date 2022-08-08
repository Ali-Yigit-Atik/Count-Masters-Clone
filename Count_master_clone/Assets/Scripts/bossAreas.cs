using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAreas : MonoBehaviour
{
    private GameObject boss;
    private bool isAttack = false;
    private GameObject player;


    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("boss");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("bossRunArea"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {
                bossBattle.isRun = true;

                //boss.GetComponent<Animator>().SetBool("inBattle", true);
                //if(isAttack == false)
                //{
                //    boss.GetComponent<Animator>().SetFloat("battleMode", 0);
                //}
                //
                //player.transform.position = Vector3.Lerp(player.transform.position, transform.position, Time.deltaTime * 0.05f);
                //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 0.05f);
            }
        }



        if (gameObject.CompareTag("bossAttackArea"))
        {
            if (other.gameObject.CompareTag("teamMember"))
            {
                bossBattle.isAttack= true;
                //boss.GetComponent<Animator>().SetFloat("battleMode", 0);
                //StartCoroutine(attack());
            }
        }
    }

    //IEnumerator attack()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    boss.GetComponent<Animator>().SetFloat("battleMode", Random.Range(1,3));
    //}
    
}
