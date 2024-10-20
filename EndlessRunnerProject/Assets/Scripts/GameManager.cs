using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public player plScript;
    stats plStats;
    public blockSpawner spawner;
    public GameObject GameOverScreen;
    float timer;
    public float difficultyTime;
    public float difficulty;
    public int charIndex;
    public GameObject[] characters;
    
    public GameObject[] buttons;

    public int TotalCoins;
    public Text totalCoinsTxt;

    int highScore;
    public Text highScoreTxt;
    // Start is called before the first frame update
    void Start()
    {
        timer = difficultyTime;
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            charIndex = PlayerPrefs.GetInt("CharIndex", 0);
            

        }
        else
        {
            charIndex=0;
            characters[charIndex].SetActive(true);
        }
        TotalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        highScore=PlayerPrefs.GetInt("HighScore",0);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            IncreaseDifficulty();
        }
        else
        {
            
            PlayerPrefs.SetInt("CharIndex", charIndex);
            highScoreTxt.text=highScore.ToString();
            
        }
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        
        charIndex = Mathf.Clamp(charIndex, 0, 7);
        totalCoinsTxt.text=" x "+TotalCoins.ToString();

    }
  public void Restart()
    {
           GameOverScreen.SetActive(true);
        
    }
    public void retry()
    {
        SceneManager.LoadScene(1);
    }
    public void menu()
    {
        plStats = plScript.GetComponent<stats>();
        TotalCoins += plStats.coin;
        if(highScore<plStats.score){
            highScore=plStats.score;
            PlayerPrefs.SetInt("HighScore",highScore);
        }
        SceneManager.LoadScene(0);
    }
    public void quit_game(){
        Application.Quit();
    }
    public void character_selection(GameObject obj){
        
        obj.SetActive(!obj.activeSelf);
        foreach(GameObject button in buttons){
            button.SetActive(!button.activeSelf);
        }
    }
    void IncreaseDifficulty()
    {
        if (timer >= Time.deltaTime)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            plScript.speed += difficulty;
            spawner.spawnTime+= difficulty;
            Debug.Log("Difficulty Increased!");

            timer = Time.deltaTime + difficultyTime;
            difficultyTime += difficulty;
        }
    }
    public void SwitchCharacter(int n)
    {
        characters[charIndex].SetActive(false);
       if(charIndex<7&&charIndex>0)
        {
            charIndex += n;
        }else if(charIndex==0)
        {
            if (n == -1)
            {
                charIndex = 7;
            }
            else
            {
                charIndex += n;
            }
        }else if(charIndex==7)
        {
            if (n == 1)
            {
                charIndex = 0;
            }
            else
            {
                charIndex += n;
            }
        }
       
        characters[charIndex].SetActive(true);
        
    }

}
