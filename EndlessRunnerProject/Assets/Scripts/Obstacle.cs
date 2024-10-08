using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string obstacleType;
    public Mesh[] buses;
    public MeshFilter filter;
    // Start is called before the first frame update
    void Start()
    {
        switch (obstacleType)
        {
            case "bus":
                filter.mesh = buses[Random.Range(0, buses.Length)];
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
