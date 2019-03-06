using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int highscore;
    public int currentScore;
    public int currentLevel = 0;
    public bool isHighscoreReach = false;
    public bool IsGameOver = false;


    public static GameController instance;
    public HelixController helixController;
    public BouncyBallController ballController;
    public UIController uiController;
    public CameraController cameraController;
    public int level;

    // Use this for initialization
    void Awake () {
		if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        }

        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
        }
     
        UIController.RestartEvent += OnRestartEvent;


    }

    public void NextLevel()
    {
        if (!IsGameOver)
        {
            level++;
            PlayerPrefs.SetInt("Level", level);
            uiController.OnLevelSucceed();
            helixController.DestroyLevel();
            helixController.ProceduralGeneration();
            ballController.OnRestartBehavior();
            cameraController.OnRestartBehavior();
        }

    }

    // Restart current stage
    public void OnRestartEvent()
    {
         helixController.OnRestartBehavior();
         ballController.OnRestartBehavior();
        cameraController.OnRestartBehavior();
        IsGameOver = false;
    }

    public void GameOver()
    {
        if (!IsGameOver)
        {
            ballController.rbBall.velocity = Vector3.zero;
            uiController.OnGameOver();
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;

        if(currentScore > highscore)
        {
            highscore = currentScore;
            isHighscoreReach = true;
            PlayerPrefs.SetInt("Highscore", currentScore);
        }
    }

 
}
