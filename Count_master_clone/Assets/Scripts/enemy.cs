using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemy : MonoBehaviour
{
    public GameObject enemySoldier; // spawn edilecek game obje
    public List<GameObject> enemies = new List<GameObject>();
    public float UnitSphereRatio = 2.5f;    
    private GameObject newEnemy;
    private float startYPosition;
    public int enemyNumbers;

    public TextMeshProUGUI enemyCount;

    private void Start()
    {

        startYPosition = transform.GetChild(0).gameObject.transform.position.y;
        enemies.Add(transform.GetChild(0).gameObject);

        spawnMember((enemyNumbers-1));

    }

    public void Update()
    {
        

        enemyCount.text = enemies.Count.ToString();

        

    }

    private Vector3 spwanPosition()
    {

        Vector3 pos = Random.insideUnitSphere * UnitSphereRatio; // ana düþmanýn etrafýna yeni karakter spawn etmek için Random.insideUnitSphere kodu kullanýldý
        Vector3 spawnPos = transform.position + pos;
        spawnPos.y = startYPosition;
        return spawnPos;


    }

    public void spawnMember(int memberSize)
    {

        for (int i = 0; i < memberSize; i++)
        {
            newEnemy = Instantiate(enemySoldier, spwanPosition(), Quaternion.identity, transform);
            newEnemy.transform.localRotation = Quaternion.Euler(0, 180, 0);
            enemies.Add(newEnemy);
        }

        

    }
}
