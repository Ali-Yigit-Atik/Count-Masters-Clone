using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class order : MonoBehaviour
{
    public static bool isNeedOrder = true;
    private Animator memberAnimator;

    private GameObject enemy_;
    private List<GameObject> enemies_;

    public void Start()
    {

        

        if (gameObject.CompareTag("soldier"))
        {
            enemy_ = gameObject.transform.parent.gameObject;
            enemies_ = enemy_.gameObject.GetComponent<enemy>().enemies;
        }

        if (gameObject.CompareTag("teamMember"))
        {
            memberAnimator = GetComponent<Animator>();
        }
    }
    private void Update()
    {

        if (gameObject.CompareTag("soldier"))
        {

            
            if (enemies_.Count < 3 && newMemberSpawn.members.Count > 10)
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, -0.8f), Time.deltaTime * 5f);
            }
            else if (enemies_.Count < 3 && newMemberSpawn.members.Count > 5)
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, -0.5f), Time.deltaTime * 5f);
            }
            else
            {
                gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 1f);
            }
            
        }

        
        

        if (isNeedOrder == true)
        {
            


            if (gameObject.CompareTag("teamMember"))
            {

                if (bossBattle.isBossDead == false)
                {
                    
                    if(newMemberSpawn.members.Count < 5 )
                    {
                        gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (1f + 2.5f / (newMemberSpawn.members.Count)));
                    }
                    else
                    {
                        gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * (0.5f + 1.8f / (newMemberSpawn.members.Count)));
                    }
                    

                    
                }

                if (bossBattle.LockOnTarget)
                {
                    memberAnimator.SetBool("inBattle", true);
                    memberAnimator.SetFloat("attack", 0);

                    var membersRotation = new Vector3(bossBattle.bossPosition.x, gameObject.transform.position.y, bossBattle.bossPosition.z) - gameObject.transform.position;
                    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(membersRotation, Vector3.up), 10f * Time.deltaTime);
                }

                if (bossBattle.isBossDead)
                {
                    StartCoroutine(winPose());
                    
                }
            }

        }

        


    }

    IEnumerator order_()
    {
        
        yield return new WaitForSeconds(3f);
        isNeedOrder = false;
        Debug.Log("order çalýþýyor");
    }

    IEnumerator winPose()
    {
        yield return new WaitForSeconds(1.5f);

        memberAnimator.SetFloat("attack", 2);

        var membersRotation2 = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z) - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(membersRotation2, Vector3.up), 10f * Time.deltaTime);
    }
}
