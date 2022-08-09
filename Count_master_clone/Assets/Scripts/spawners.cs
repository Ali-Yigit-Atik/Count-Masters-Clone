using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawners : MonoBehaviour
{
    public enum multipleOrAddiable
    {
        multiple,
        addiable
    }

    public multipleOrAddiable newMembers;
    public int newSpawnSize;
    private newMemberSpawn newMemberSpawn_;
    private bool isGateActive = true;
    public GameObject otherSpawner;
    private spawners spawners_;
    public TextMeshProUGUI UISpawn;
    private bool avoidSpawnBug =false; //Bazen karakterler hem �arpma hemde toplama spawn alan�na �ok h�zl� girebiliyor ve ikisinidende etkilenebilliyor
                                       // Bundan ka��nmak i�in yap�lan i�lemler aras�nda hesaplamalr i�in k���k bir zaman dilimi b�rakt�m

    private void Start()
    {
        newMemberSpawn_ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<newMemberSpawn>();
       spawners_ = otherSpawner.gameObject.GetComponent<spawners>();

        switch (newMembers)
        {
            case multipleOrAddiable.addiable:
                UISpawn.text = "+" + newSpawnSize.ToString();
                break;
            case multipleOrAddiable.multiple:
                UISpawn.text = "x" + newSpawnSize.ToString();
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("teamMember") && isGateActive  && gameObject.CompareTag("spawner"))
        {
            spawners_.enabled = false;
            otherSpawner.tag = "Untagged";
            StartCoroutine("Gate");


            if (avoidSpawnBug == true)
            {
                switch (newMembers)
                {
                    case multipleOrAddiable.multiple:

                        newMemberSpawn_.spawnMember(newMemberSpawn.members.Count * (newSpawnSize - 1));
                        break;

                    case multipleOrAddiable.addiable:

                        newMemberSpawn_.spawnMember(newSpawnSize);
                        break;

                }
            }
            
        }
        
    
    }

    IEnumerator Gate()
    {
        isGateActive = false;
        avoidSpawnBug = true;
        
        yield return new WaitForSeconds(1.5f);
        //isGateActive = true;
    }
}
