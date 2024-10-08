using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    [SerializeField]
    public int coin;
    public Text coinDisplay;

    public int score;
    public Text scoreDisplay;
    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer=1f;
    }

    // Update is called once per frame
    void Update()
    {   
        if (timer>0){
            timer-=Time.deltaTime;
        }else{
            timer=1f;
            score+=1;
        }
        coinDisplay.text="x "+coin.ToString();
        scoreDisplay.text=score.ToString();
    }
}
