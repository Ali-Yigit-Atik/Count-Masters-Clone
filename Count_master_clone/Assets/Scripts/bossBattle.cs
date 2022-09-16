using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class bossBattle : MonoBehaviour
{

    private Image healthBar;
    private float maxHealthPercent = 100;
    private float HealthPercent;
    public TextMeshProUGUI bossHealthCount;
    public float bossHealth; //
    public static Animator bossAnimator;
    private GameObject player;
    public static bool isRun = false;
    public static bool isAttack = false;
    private int attaackMode;
    public static Vector3 bossPosition; // Bazý teamMember'lar dövüþ esnasýnda koþmaya devam ediyordu bunu düzeltmek için direk teammember içinde bulunan
                                        // bir script üzerinden(order) animasyon ayarý yapmak için bossPosition'o oluþturdum. Ayrýca yine bazý teamMember'lar
                                        // doðru yöne bakmýyordu bunun içinde order scripti üzerinden Quaternion.Slerp komutuyla vucut poziyonu ve bakýþ açýsý
                                        //tanýmladým

    public static bool LockOnTarget = false;  
    private GameObject bossHitArea;   
       
    public static bool isBossDead = false;
    private bool isDistanceLessThan15 = false;
    private bool isDistanceLessThan3 = false;


    void Start()
    {


        HealthPercent = maxHealthPercent / bossHealth;
        healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Image>();
        bossAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossHitArea = GameObject.FindGameObjectWithTag("bossHitArea");      
        bossHitArea.SetActive(false);
    }

    
    void Update()
    {
        
        bossHealthCount.text =  (bossHealth).ToString();
        healthBar.fillAmount = (bossHealth * HealthPercent)/100;
        bossPosition = transform.position;


        if (newMemberSpawn.members.Count > 0 || isBossDead == false)
        {


            foreach (var stickMan in newMemberSpawn.members)
            {
                var stickManDistance = stickMan.transform.position - transform.position;


                if (stickManDistance.sqrMagnitude < 15 * 15)
                {
                    isDistanceLessThan15 = true;
                }

                if (stickManDistance.sqrMagnitude < 3.5 * 3.5)
                {

                    isDistanceLessThan3 = true;


                }

            }
        }

        letBattle();

        if (LockOnTarget)
        {
            
            bossHitArea.SetActive(true);
            attaackMode = Random.Range(1, 4);
            bossAnimator.SetInteger("attackMode", attaackMode);
            
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

    private void letBattle()
    {
        if (isDistanceLessThan15 )
        {
            PlayerController.isbattle = true;
            bossAnimator.SetBool("inBattle", true);                        
            bossAnimator.SetInteger("attackMode", 0);

            
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0, 0, 0.75f), 0.5f * Time.deltaTime);
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position + new Vector3(0, 0, -0.75f), Time.deltaTime * 0.5f);
        }

        if (isDistanceLessThan3)
        {
            LockOnTarget = true;
            PlayerController.isbattle = true;

            var bossRotation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bossRotation, Vector3.up), 10f * Time.deltaTime);

            

        }
    }

}
