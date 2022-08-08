using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newMemberSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject member; // spawn edilecek game obje
    public static List<GameObject> members = new List<GameObject>();
    public float UnitSphereRatio = 2.5f;
    public static newMemberSpawn newMemberSpawnCls;
    private GameObject newMember;
    private float startYPosition;

    public TextMeshProUGUI memberCount;

    private void Start()
    {

        startYPosition = transform.GetChild(1).gameObject.transform.position.y;
        members.Add(transform.GetChild(1).gameObject);
        
        
        
    }

    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            spawnMember(10);
        }

        memberCount.text = members.Count.ToString();

    }
    private Vector3 spwanPosition()
    {

        Vector3 pos = Random.insideUnitSphere * UnitSphereRatio ; // ana karakterin etrafýna yeni karakter spawn etmek için Random.insideUnitSphere kodu kullanýldý
        Vector3 spawnPos = transform.position + pos;
        spawnPos.y = startYPosition;
        return spawnPos;

              
        

    }

    public void spawnMember(int memberSize)
    {

        for (int i = 0; i < memberSize; i++)
        {
            newMember = Instantiate(member, spwanPosition(), Quaternion.identity, transform);

            members.Add(newMember);
        }

        //foreach (var stickman_rb in members)
        //{
        //
        //    Vector3 diection = transform.GetChild(1).gameObject.transform.position - stickman_rb.transform.position;
        //    stickman_rb.transform.rotation = Quaternion.Slerp(stickman_rb.transform.rotation, Quaternion.LookRotation(diection), Time.deltaTime);
        //
        //
        //}

    }

    //private Vector3 orderPosition;
    //
    //private void Start()
    //{
    //    orderPosition = gameObject.transform.parent.transform.GetChild(1).transform.localPosition;
    //}
    //void Update()
    //{
    //    transform.localPosition = Vector3.MoveTowards(transform.localPosition, orderPosition, Time.deltaTime* 5 );
    //}
}
