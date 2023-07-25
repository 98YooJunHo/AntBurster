using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monsterPrefab;

    private bool firstMob;
    private float timeAfterSpawn;
    private float spawnTimeMin;
    private float spawnTimeMax;
    private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        firstMob = false;
        spawnTimeMin = 2f;
        spawnTimeMax = 5f;
        spawnRate = Random.Range(spawnTimeMin, spawnTimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if(!firstMob)
        {
            FirstSpawn();
        }

        if(firstMob)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        timeAfterSpawn += Time.deltaTime;
        if(timeAfterSpawn > spawnRate)
        {
            timeAfterSpawn = 0;
            spawnRate = Random.Range(spawnTimeMin, spawnTimeMax);
            GameObject monster
                = Instantiate(monsterPrefab, transform.position, transform.rotation, GameObject.Find("UI").transform);
        }
    }

    void FirstSpawn()
    {
        timeAfterSpawn += Time.deltaTime;
        if (timeAfterSpawn > 3.5f)
        {
            GameObject monster 
                = Instantiate(monsterPrefab, transform.position, transform.rotation, GameObject.Find("UI").transform);
            timeAfterSpawn = 0;
            firstMob = true;
        }
    }
}
