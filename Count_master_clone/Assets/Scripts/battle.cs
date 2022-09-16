using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // ElementAt komutunu kullanmak için tanýmladým

public class battle : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy_;
    private Animator soldierAnimator;
    private List<GameObject> enemies_;

    private int battleDistance;
    private bool letThemBattle = false;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
        enemy_ = gameObject.transform.parent.gameObject;
        soldierAnimator = GetComponent<Animator>();

        enemies_ = enemy_.gameObject.GetComponent<enemy>().enemies;
    }

    public void Update()
    {
        

        if(newMemberSpawn.members.Count <= 0)
        {
            
            StartCoroutine(DontRun());
        }

        if(newMemberSpawn.members.Count < 30) 
        {
            battleDistance = 8;
        }
        if (newMemberSpawn.members.Count >= 30 && newMemberSpawn.members.Count < 70)
        {
            battleDistance = 11;
        }
        if (newMemberSpawn.members.Count >= 70)
        {
            battleDistance = 13;
        }

        for (int i =0; i<newMemberSpawn.members.Count; i++)
        {
            for (int j = 0; j < enemies_.Count; j++) 
            {
                if(Vector3.Magnitude(newMemberSpawn.members[i].transform.position - enemies_[j].transform.position) < battleDistance)
                {                           
                    if (newMemberSpawn.members.Count > 0 && enemies_.Count > 0) 
                    {
                        letThemBattle = true;                        
                    }
                }
                
            }
        }

        letBattle();

    }

    private void letBattle()
    {
        if (letThemBattle)
        {

            PlayerController.isbattle = true;
            
            player.transform.position = Vector3.MoveTowards(player.transform.position, enemy_.transform.position, Time.deltaTime * 0.65f );
            enemy_.transform.position = Vector3.MoveTowards(enemy_.transform.position, player.transform.position, Time.deltaTime * 0.65f );

            foreach (var x in enemies_)
            {
                x.GetComponent<Animator>().SetBool("run", true);
            }

            if(newMemberSpawn.members.Count<=0 || enemies_.Count<=0)
            {
                letThemBattle = false;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("soldier"))
        {
            if (collision.gameObject.CompareTag("teamMember"))
            {

                this.gameObject.tag = "Untagged";
                collision.gameObject.tag = "Untagged";

                for(int i=0; i< newMemberSpawn.members.Count; i++)
                {
                    if(newMemberSpawn.members.ElementAt(i).name == collision.gameObject.name)
                    {
                        newMemberSpawn.members.RemoveAt(i);
                        collision.gameObject.SetActive(false);
                        break;
                    }
                }
                

                for(int i=0; 0< enemies_.Count; i++)
                {
                    if(enemies_.ElementAt(i).name == gameObject.name)
                    {
                        
                        if(enemies_.Count <= 1)
                        {
                            PlayerController.isbattle = false;
                            order.isNeedOrder = true;
                        }
                        enemies_.RemoveAt(i);
                        gameObject.SetActive(false);
                        break;
                    }
                }
                
            }
        }
    }

    IEnumerator DontRun()
    {
        soldierAnimator.SetBool("run", false);
        yield return new WaitForSeconds(0.7f);
    }
}
