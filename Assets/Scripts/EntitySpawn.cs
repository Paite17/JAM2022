using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawn : MonoBehaviour
{
    [SerializeField] private GameObject entityObj;
    public int spawnCount;
    public float spawnTimer;
    public float amountOfTimeToSpawn;
    public int round;
    public int maxRounds;
    public bool startTrigger;

    // Update is called once per frame
    void Update()
    {
        // count up
        spawnTimer += Time.deltaTime;

        // check when timer is reached and hasn't exceeded number of rounds
        if (spawnTimer > amountOfTimeToSpawn && round < maxRounds && startTrigger == true)
        {
            
            for (int i = 0; i < spawnCount; i++)
            {
                // add some slight variation in spawn positions
                int xVar = Random.Range(1, 3);
                int yVar = Random.Range(1, 3);
                Vector3 spawnPos = new Vector3(gameObject.transform.position.x + xVar, gameObject.transform.position.y + yVar, 1);
                GameObject tempSpawn = Instantiate(entityObj, spawnPos, transform.rotation); 
            } 

            spawnTimer = 0;
            round++;
        }
    }
}
