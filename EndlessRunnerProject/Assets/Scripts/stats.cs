using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    [SerializeField]
    public int score;
    public Text scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
       
        scoreDisplay.text="x "+score.ToString();
    }
}
