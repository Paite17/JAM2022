using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour
{
    [SerializeField] private GameObject entityObj;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float amountOfTimeToSpawn;
    [SerializeField] private int round;
    [SerializeField] private int maxRounds;

    // Update is called once per frame
    void Update()
    {
        // count up
        spawnTimer += Time.deltaTime;

        // check when timer is reached and hasn't exceeded number of rounds
        if (spawnTimer > amountOfTimeToSpawn && round < maxRounds)
        {
            
            for (int i = 0; i < spawnCount; i++)
            {
                // add some slight variation in spawn positions
                int xVar = Random.Range(1, 5);
                int yVar = Random.Range(1, 5);
                Vector3 spawnPos = new Vector3(gameObject.transform.position.x + xVar, gameObject.transform.position.y + yVar, 1);
                GameObject tempSpawn = Instantiate(entityObj, spawnPos, transform.rotation); 
            } 

            spawnTimer = 0;
            round++;
        }
    }
}
