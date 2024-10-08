using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSpawner : MonoBehaviour
{
    float timer;
    public float spawnTime;
    public Transform playerPos;
    float dist=10f;
    public GameObject lastBlock;
    public float noOfSpawns;
    public GameObject block;
    public float safeDist=40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(playerPos.position.z-lastBlock.transform.position.z)<safeDist) 
        {
            // Debug.Log("Spawn");
            for (int i = 0; i <= noOfSpawns; i++)
            {
                var obj = Instantiate(block, lastBlock.transform.position + new Vector3(0f, 0f, dist), Quaternion.Euler(0f, 0f, 0f));
                obj.GetComponent<Block>().CoinSpawning(lastBlock.transform.position.z + dist, Random.Range(0, 3));
                lastBlock = obj;
            }
           // timer = Time.deltaTime + spawnTime;
        }
    }
}
