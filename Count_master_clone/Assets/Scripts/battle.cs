using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // ElementAt komutunu kullanmak için tanýmladým

public class battle : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy_;
    private Animator soldierAnimator;

    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy_ = GameObject.FindGameObjectWithTag("enemy");
        soldierAnimator = GetComponent<Animator>();
    }

    public void Update()
    {
        //Vector3 direction = player.transform.position - gameObject.transform.position;
        //RaycastHit hit = Physics.Raycast(transform.position, transform.forward, 5f);

        if(newMemberSpawn.members.Count <= 0)
        {
            soldierAnimator.SetBool("run", false);
        }
    
        for(int i =0; i<newMemberSpawn.members.Count; i++)
        {
            for (int j = 0; i < enemy.enemies.Count; j++)
            {
                if(Vector3.Magnitude(newMemberSpawn.members[i].transform.position - enemy.enemies[j].transform.position) < 7)
                {
    
                    
    
                    if (newMemberSpawn.members.Count > 0 && enemy.enemies.Count > 0) // && enemy.enemies[j] != null && newMemberSpawn.members[i] != null
                    {
                        

                        
                        player.transform.position = Vector3.Lerp(player.transform.position, enemy_.transform.position, Time.deltaTime * 0.06f /(enemy.enemies.Count + newMemberSpawn.members.Count));
                        enemy_.transform.position = Vector3.Lerp(enemy_.transform.position, player.transform.position, Time.deltaTime * 0.06f /(enemy.enemies.Count + newMemberSpawn.members.Count));

                        foreach (var x in enemy.enemies)
                        {
                            x.GetComponent<Animator>().SetBool("run", true);
                        }


                        //newMemberSpawn.members[i].transform.position = Vector3.MoveTowards(newMemberSpawn.members[i].transform.position, enemy.enemies[j].transform.position, Time.deltaTime * 0.5f);
                        //enemy.enemies[j].transform.position = Vector3.MoveTowards(enemy.enemies[j].transform.position, newMemberSpawn.members[i].transform.position, Time.deltaTime * 0.5f);

                        PlayerController.isbattle = true;



                        ////player.transform.position = Vector3.MoveTowards(player.transform.position, enemy_.transform.position, Time.deltaTime * 0.2f);
                        ////enemy_.transform.position = Vector3.MoveTowards(enemy_.transform.position, player.transform.position, Time.deltaTime *0.2f);
                        ////player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + Time.deltaTime * 0.5f);
                        //
                        //player.transform.position = Vector3.Lerp(player.transform.position, enemy_.transform.position, Time.deltaTime * 0.001f);
                        //enemy_.transform.position = Vector3.Lerp(enemy_.transform.position, player.transform.position, Time.deltaTime * 0.001f);
                        ////player.transform.position = new Vector3(player.transform.position.x + Time.deltaTime * 0.2f, player.transform.position.y, player.transform.position.z + Time.deltaTime * 0.2f);
                        ////enemy_.transform.position = new Vector3(enemy_.transform.position.x + Time.deltaTime * 0.2f, enemy_.transform.position.y, enemy_.transform.position.z + Time.deltaTime * 0.2f);
                        //
                        //
                        //foreach (var x in newMemberSpawn.members)
                        //{
                        //    x.transform.position = Vector3.Lerp(x.transform.position, enemy_.transform.position + new Vector3(0,0,1), Time.deltaTime*0.1f);
                        //    x.transform.localPosition = Vector3.Lerp(x.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 0.1f);
                        //
                        //}
                        //
                        //
                        //
                        //foreach (var x in enemy.enemies)
                        //{
                        //    x.transform.position = Vector3.Lerp(x.transform.position, player.transform.position + new Vector3(0, 0, -1), Time.deltaTime*0.1f);
                        //    x.transform.localPosition = Vector3.Lerp(x.transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 0.1f);
                        //}




                    }

                }

                
            }
        }

        
    }

    //private void FixedUpdate()
    //{
    //    
    //
    //    for (int i = 0; i < newMemberSpawn.members.Count; i++)
    //    {
    //        for (int j = 0; i < enemy.enemies.Count; j++)
    //        {
    //            if (Vector3.Magnitude(newMemberSpawn.members[i].transform.position - enemy.enemies[j].transform.position) < 7)
    //            {
    //
    //                
    //
    //                if (newMemberSpawn.members.Count > 0 && enemy.enemies.Count > 0)
    //                {
    //                    PlayerController.isbattle = true;
    //                    transform.parent.gameObject.transform.position = Vector3.MoveTowards(transform.parent.gameObject.transform.position, player.transform.position, Time.fixedDeltaTime);
    //                }
    //                //else if(newMemberSpawn.members.Count == 0 || enemy.enemies.Count == 0)
    //                else
    //                {
    //                    PlayerController.isbattle = false;
    //                    break;
    //                }
    //            }
    //        }
    //    }
    //}







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
                

                for(int i=0; 0<enemy.enemies.Count; i++)
                {
                    if(enemy.enemies.ElementAt(i).name == gameObject.name)
                    {
                        
                        if(enemy.enemies.Count == 1)
                        {
                            PlayerController.isbattle = false;
                            order.isNeedOrder = true;
                        }
                        enemy.enemies.RemoveAt(i);
                        gameObject.SetActive(false);
                        break;
                    }
                }
                
            }
        }
    }
}
