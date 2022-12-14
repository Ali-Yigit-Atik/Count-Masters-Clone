using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class newMemberSpawn : MonoBehaviour
{
    

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
        if (Input.GetKeyDown("a")) // Spawn ?al???yor mu diye kontrol anahtar?
        {
            spawnMember(10);
        }

        memberCount.text = members.Count.ToString();

    }
    private Vector3 spwanPosition()
    {

        Vector3 pos = Random.insideUnitSphere * UnitSphereRatio ; // ana karakterin etraf?na yeni karakter spawn etmek i?in Random.insideUnitSphere kodu kullan?ld?
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

        

    }

    
}
