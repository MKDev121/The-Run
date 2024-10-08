using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    int noOfObstacles = 0;
    public GameObject[] obstacles;
    public Transform[] lane;
    public GameObject coin;
    
    public float posZ;
    public float destroyTime;
    Transform player;
    Vector3 offset;
    public float playerDist;
    public GameObject[] buildings;
    public Transform[] buildingPos;
    public float coinRadius=2.5f;
   
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player").GetComponent<Transform>();
       
        noOfObstacles = Random.Range(0, 3);
        switch (noOfObstacles)
        {
            case 0:
                //DoNothing
                break;
            case 1:
                Instantiate(obstacles[Random.Range(0, 2)], lane[Random.Range(0, 3)].position + new Vector3(0f,  0f, Random.Range(-posZ, posZ)), Quaternion.EulerAngles(0f,0f,0f),transform);        
                break;
            case 2:
                Instantiate(obstacles[Random.Range(0, 2)], lane[Random.Range(0,3)].position+new Vector3(0f,0f,Random.Range(-posZ,posZ)), Quaternion.EulerAngles(0f, 0f, 0f),transform);
               // Instantiate(obstacles[Random.Range(0, 2)], lane[Random.Range(0, 3)].position + new Vector3(0f, 0f, Random.Range(-posZ, posZ)), Quaternion.EulerAngles(0f, 0f, 0f), transform);
                break;
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(buildings[Random.Range(0, buildings.Length)], buildingPos[i].position, Quaternion.Euler(0f, 180f, 0f),transform);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        offset=player.position-transform.position;
        if (offset.z > playerDist)
        {
            Destroy(gameObject);
        }
    }
    public void CoinSpawning(float gap,int laneIndex)
    {
        
        for(int i =0; i < 5; i++)
        {
           var obj = Instantiate(coin, new Vector3(lane[laneIndex].position.x, 0f, -5f +(2*i)+gap), Quaternion.Euler(90f,0f,0f),transform);
            Collider[] hits = Physics.OverlapSphere(obj.transform.position, coinRadius);  

        }
    }

    
}
