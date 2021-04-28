using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game Controller staff")]
    [SerializeField] private Hero hero;
    [SerializeField] private int scoreToWin;
    [SerializeField] private MenuManager menuManager;

    [Header("UI Staff")] 
    [SerializeField] private Text time;
    [SerializeField] private Text lives;
    [SerializeField] private Text scores;
    
    private int currentScore = 0;
    private bool gameEnd = false;
    
    private static GameController sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        DisplayScores();
    }

    private void Update()
    {
        if (!gameEnd)
        {
            DisplayTime();
            DisplayLives();
        }
    }

    private void DisplayTime()
    {
        TimeSpan timeElapsed = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        time.text = "Time: " + timeElapsed.ToString(@"mm\:ss");
    }

    private void DisplayLives()
    {
        lives.text = "Lives: " + hero.Lives;
        if (hero.Lives <= 0)
        {
            LoseGame();
        }
    }

    private void DisplayScores()
    {
        scores.text = "Scores: " + currentScore;
    }

    public static void AddScore(int amount)
    {
        sharedInstance.currentScore += amount;
        sharedInstance.DisplayScores();
        
        if (sharedInstance.currentScore >= sharedInstance.scoreToWin)
        {
            sharedInstance.WinGame();
        }
    }

    private void WinGame()
    {
        gameEnd = true;
        menuManager.OpenWinMenu(hero.Lives);
        //Debug.Log("You win!");
    }

    private void LoseGame()
    {
        gameEnd = true;
        menuManager.OpenLoseMenu();
        //Debug.Log("You lose!");
    }
}
