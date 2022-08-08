using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class bossBattle : MonoBehaviour
{
    public TextMeshProUGUI bossHealthCount;
    public int bossHealth;
    private Animator bossAnimator;
    private GameObject player;
    public static bool isRun = false;
    public static bool isAttack = false;
    private int attaackMode;


    private bool LockOnTarget = false;
    private Transform target;

    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthCount.text = bossHealth.ToString();


        //for (int i = 0; i < newMemberSpawn.members.Count; i++)
        //{
        //    if (Vector3.Magnitude(newMemberSpawn.members[i].transform.position - gameObject.transform.position) < 8)
        //    {
        //        PlayerController.isbattle = true;
        //        bossAnimator.SetBool("inBattle", true);
        //        bossAnimator.SetFloat("battleMode", 0);
        //
        //        player.transform.position = Vector3.Lerp(player.transform.position, transform.position, Time.deltaTime * 0.05f );
        //        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 0.05f );
        //
        //        foreach (var x in newMemberSpawn.members)
        //        {
        //            x.GetComponent<order>().enabled = false;
        //        }
        //
        //        if ( Vector3.Magnitude(newMemberSpawn.members[i].transform.position - gameObject.transform.position) < 8
        //            && Vector3.Magnitude(newMemberSpawn.members[i].transform.position - gameObject.transform.position) >2.5)
        //        {
        //            bossAnimator.SetFloat("battleMode", 0);
        //
        //        }
        //
        //
        //
        //        if (Vector3.Magnitude(newMemberSpawn.members[i].transform.position - gameObject.transform.position) < 2.5)
        //        {
        //            bossAnimator.SetFloat("battleMode", Random.Range(1, 3));
        //
        //        }
        //    }
        //}


        //if(isRun == true && isAttack == false)
        //{
        //    PlayerController.isbattle = true;
        //    bossAnimator.SetBool("inBattle", true);
        //    bossAnimator.SetFloat("battleMode", 0);
        //
        //    player.transform.position = Vector3.Lerp(player.transform.position, transform.position, Time.fixedDeltaTime * 0.2f);
        //    transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.fixedDeltaTime * 0.7f);
        //}
        //
        //if(isAttack == true)
        //{
        //    bossAnimator.SetFloat("battleMode", 3);
        //    
        //    foreach (var x in newMemberSpawn.members)
        //    {
        //        x.GetComponent<Animator>().SetBool("inBattle", true);
        //        x.GetComponent<Animator>().SetFloat("attack", 0);
        //    }
        //
        //}



        if (newMemberSpawn.members.Count > 0)
            foreach (var stickMan in newMemberSpawn.members)
            {
                var stickManDistance = stickMan.transform.position - transform.position;

                if (stickManDistance.sqrMagnitude < 15 * 15 )
                {
                    target = stickMan.transform;
                    bossAnimator.SetBool("inBattle", true);
                    PlayerController.isbattle = true;
                    //bossAnimator.SetFloat("battleMode", 0);
                    bossAnimator.SetInteger("attackMode", 0);

                    transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
                    player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.fixedDeltaTime * 0.2f);
                }

                if (stickManDistance.sqrMagnitude < 3.2 * 3.2)
                    LockOnTarget = true;

            }

        if (LockOnTarget)
        {
            var bossRotation = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);

            //  for (int i = 0; i < newMemberSpawn.members.Count; i++)
            //      if (!newMemberSpawn.members.ElementAt(i).GetComponent<newMemberSpawn>().members)
            //          newMemberSpawn.members.RemoveAt(i);



            foreach (var member in newMemberSpawn.members) 
            {
                var membersRotation = new Vector3(transform.position.x, member.transform.position.y, transform.position.z) - member.transform.position;
                member.transform.rotation = Quaternion.Slerp(member.transform.rotation, Quaternion.LookRotation(membersRotation, Vector3.up), 10f * Time.deltaTime);
                
                member.GetComponent<Animator>().SetBool("inBattle", true);
                member.GetComponent<Animator>().SetFloat("attack", 0);
            }

            //bossAnimator.SetFloat("battleMode", attaackMode);
            //StartCoroutine(attack());
            bossAnimator.SetInteger("attackMode", Random.Range(1, 4));

        }

       

    }

    IEnumerator attack()
    {
        //yield return new WaitForSeconds(3f);
        //bossAnimator.SetFloat("battleMode", 1);
        //yield return new WaitForSeconds(6f);
        //bossAnimator.SetFloat("battleMode", 2);
        //yield return new WaitForSeconds(6f);
        //bossAnimator.SetFloat("battleMode", 3);
        //yield return new WaitForSeconds(6f);

        yield return new WaitForSeconds(3f);

        //bossAnimator.SetInteger("attackMode", Random.Range(0, 3));
        //attaackMode = Random.Range(1, 4);

        //yield return new WaitForSeconds(4f);
        //bossAnimator.SetFloat("battleMode", Random.Range(1,4));




    }

}
