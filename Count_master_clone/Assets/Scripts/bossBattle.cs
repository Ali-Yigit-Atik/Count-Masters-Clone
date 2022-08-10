using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class bossBattle : MonoBehaviour
{
    public TextMeshProUGUI bossHealthCount;
    public int bossHealth;
    public static Animator bossAnimator;
    private GameObject player;
    public static bool isRun = false;
    public static bool isAttack = false;
    private int attaackMode;
    public static Vector3 bossPosition; // Baz� teamMember'lar d�v�� esnas�nda ko�maya devam ediyordu bunu d�zeltmek i�in direk teammember i�inde bulunan
                                        // bir script �zerinden(order) animasyon ayar� yapmak i�in bossPosition'o olu�turdum. Ayr�ca yine baz� teamMember'lar
                                        // do�ru y�ne bakm�yordu bunun i�inde order scripti �zerinden Quaternion.Slerp komutuyla vucut poziyonu ve bak�� a��s�
                                        //tan�mlad�m


    public static bool LockOnTarget = false;
    private Transform target;

    private GameObject bossHitArea;
    private int beforeAttacked = 0;
    private GameObject closetsEnemy;
    private Vector3 minumumstickManDistance = new Vector3(15,15,15) ;

    public static bool isBossDead = false;

    void Start()
    {
        bossAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossHitArea = GameObject.FindGameObjectWithTag("bossHitArea");
        //bossHitArea.GetComponent<obstacles>().enabled = false;

        bossHitArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthCount.text = bossHealth.ToString();
        bossPosition = transform.position;

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
                    //target = stickMan.transform;
                    bossAnimator.SetBool("inBattle", true);
                    PlayerController.isbattle = true;
                    //bossAnimator.SetFloat("battleMode", 0);
                    bossAnimator.SetInteger("attackMode", 0);

                    //transform.position = Vector3.MoveTowards(transform.position, target.position, 1f * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 0, 1.5f), 0.8f * Time.deltaTime);
                    player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * 0.2f);
                }

                if (stickManDistance.sqrMagnitude < 3.5 * 3.5)
                {
                    LockOnTarget = true;
                    PlayerController.isbattle = true;


                    //if (stickManDistance.sqrMagnitude < minumumstickManDistance.sqrMagnitude && stickMan.gameObject.activeSelf)
                    //{
                    //    minumumstickManDistance = stickManDistance;
                    //    closetsEnemy = stickMan;
                    //
                    //
                    //    target = closetsEnemy.transform;
                    //}
                    //else if(stickMan.gameObject.activeSelf == false)
                    //{
                    //    minumumstickManDistance = new Vector3(15, 15, 15);
                    //    closetsEnemy = null;
                    //    break;
                    //}

                    //var bossRotation = new Vector3(target.position.x, target.position.y, target.position.z) - transform.position;


                    //var bossRotation = new Vector3(closetsEnemy.transform.position.x, transform.position.y, closetsEnemy.transform.position.z) - transform.position;
                    var bossRotation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);

                    break;// Burdaki break'i koyma amac�m yukar�da boss'un hedefi olarak player.transform.position + new Vector3(0, 0, 1.5f) yazmam
                          // break'i koymasam boss ve teamMember'lar s�rekli birbirlerini itecekleri i�in boss s�rekli geri kayacak
                          //new Vector3(0, 0, 1.5f) yazama gerekcem ise teamMember'lar�n hepsi player'�n merkezine hareket ediyor
                          // boss direk merkezde olursa team meberlar merkeze hareket edemez ve birbirlerinide ittiklerinden boss'un arkas�nda yani
                          // vuru� a��s�n�n d���na ��kabilirler.

                }

            }

        if (LockOnTarget)
        {
            //var bossRotation = new Vector3(target.position.x, target.position.y, target.position.z) - transform.position;

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);

            //  for (int i = 0; i < newMemberSpawn.members.Count; i++)
            //      if (!newMemberSpawn.members.ElementAt(i).GetComponent<newMemberSpawn>().members)
            //          newMemberSpawn.members.RemoveAt(i);


            //Ayn� kodu order'�n i�ine team member'lar� etkileyecek �ekilde yazd�m buraya yazmaya gerek yok. Orda �al��t�r�nca bug olamdan �al���yor
            //foreach (var member in newMemberSpawn.members) 
            //{
            //    var membersRotation = new Vector3(transform.position.x, member.transform.position.y, transform.position.z) - member.transform.position;
            //    member.transform.rotation = Quaternion.Slerp(member.transform.rotation, Quaternion.LookRotation(membersRotation, Vector3.up), 10f * Time.deltaTime);
            //    
            //    member.GetComponent<Animator>().SetBool("inBattle", true);
            //    member.GetComponent<Animator>().SetFloat("attack", 0);
            //}



            //bossAnimator.SetFloat("battleMode", attaackMode);
            //StartCoroutine(attack());

            bossHitArea.SetActive(true);
            attaackMode = Random.Range(1, 4);
            bossAnimator.SetInteger("attackMode", attaackMode);
            //hitTeamMeber();
            //StartCoroutine(hitTeamMeber());

            



        }

       
       if(newMemberSpawn.members.Count <= 0)
       {
            bossAnimator.SetInteger("attackMode", 4);
       }
       else if(bossHealth <= 0)
       {
            bossAnimator.SetInteger("attackMode", 5);

            LockOnTarget = false;
            isBossDead = true;

            
       }

    }

    //IEnumerator hitTeamMeber()
    //{
    //
    //    //bossHitArea.GetComponent<obstacles>().enabled = false;
    //    //yield return new WaitForSeconds(2.1f);
    //    //bossHitArea.GetComponent<obstacles>().enabled = true;
    //    bool isFirstTime = true;
    //
    //    if(attaackMode != 0 && isFirstTime)
    //    {
    //        beforeAttacked = attaackMode;
    //        isFirstTime = false;
    //    }
    //    //if (bossAnimator.GetInteger("attackMode") != beforeAttacked)
    //    //{
    //    //    bossHitArea.GetComponent<obstacles>().enabled = true;
    //    // yield return new WaitForSeconds(0.2f);
    //    //    bossHitArea.GetComponent<obstacles>().enabled = false;
    //    //    beforeAttacked = attaackMode;
    //    //}
    //
    //    if(bossAnimator.GetCurrentAnimatorStateInfo(beforeAttacked).normalizedTime > 1)
    //    {
    //        bossHitArea.GetComponent<obstacles>().enabled = true;
    //        yield return new WaitForSeconds(0.2f);
    //        bossHitArea.GetComponent<obstacles>().enabled = false;
    //        beforeAttacked = attaackMode;
    //    }
    //
    //}


    //public void hitTeamMeber()
    //{
    //
    //    if (bossAnimator.GetInteger("attackMode") != attaackMode)
    //    {
    //        bossHitArea.GetComponent<obstacles>().enabled = true;
    //    }
    //    else
    //    {
    //        bossHitArea.GetComponent<obstacles>().enabled = false;
    //    }
    //    beforeAttacked = attaackMode;
    //}


    //IEnumerator attack()
    //{
    //    //yield return new WaitForSeconds(3f);
    //    //bossAnimator.SetFloat("battleMode", 1);
    //    //yield return new WaitForSeconds(6f);
    //    //bossAnimator.SetFloat("battleMode", 2);
    //    //yield return new WaitForSeconds(6f);
    //    //bossAnimator.SetFloat("battleMode", 3);
    //    //yield return new WaitForSeconds(6f);
    //
    //    yield return new WaitForSeconds(3f);
    //
    //    //bossAnimator.SetInteger("attackMode", Random.Range(0, 3));
    //    //attaackMode = Random.Range(1, 4);
    //
    //    //yield return new WaitForSeconds(4f);
    //    //bossAnimator.SetFloat("battleMode", Random.Range(1,4));
    //
    //
    //
    //
    //}

}
