using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int swipeSpeed = 10;
    public int forwardSpeed = 5;
    private bool move = false;
    private bool dontMoveLeft_ = false;
    private bool dontMoveRight_ = false;
    public static bool isbattle = false;
    private float startYPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move == true && isbattle == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * forwardSpeed);
            Debug.Log("count = " + newMemberSpawn.members.Count);
        }


        if (Input.GetMouseButton(0) && isbattle== false)
        {
            move = true;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitPosition, 100))
              
            {
                Vector3 sweepPosition = hitPosition.point;
                sweepPosition.y = transform.position.y;
                sweepPosition.z = transform.position.z;

                for(int i =0; i <= newMemberSpawn.members.Count-1; i++)
                {
                    if(newMemberSpawn.members[i].gameObject.transform.position.x > sweepPosition.x)
                    {
                        dontMoveLeft_ = true;
                        //dontMoveRight = true;
                    }
                    else
                    {
                        dontMoveLeft_ = false;
                        //dontMoveRight = false;
                    }


                    if (newMemberSpawn.members[i].gameObject.transform.position.x < sweepPosition.x)
                    {
                        dontMoveRight_ = true;
                        //dontMoveLeft = true;
                    }
                    else
                    {
                        dontMoveRight_ = false;
                        //dontMoveLeft = false;
                    }


                }

                
                if (wall.dontMoveLeft == true && dontMoveLeft_ == true)
                {
                    transform.position = transform.position;
                    Debug.Log("left left");
                
                }
                else if (wall.dontMoveRight == true && dontMoveRight_ == true)
                {
                    //transform.position = Vector3.MoveTowards(transform.position, transform.position, Time.deltaTime * swipeSpeed);
                    transform.position = transform.position;
                    Debug.Log("right right");
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, sweepPosition, Time.deltaTime * swipeSpeed);
                }
               


            }
        }

       
    }


}
