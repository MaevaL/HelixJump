using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int highscore;
    public int currentScore;
    public int currentLevel = 0;
    public bool isHighscoreReach = false;

    public delegate void RestartEventHandler();
    public event RestartEventHandler RestartEvent;

    public static GameController instance;
    public HelixController helixController;
    public BouncyBallController ballController;

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

        highscore = PlayerPrefs.GetInt("Highscore");
        
	}

    public void NextLevel()
    {
        Debug.Log("next level reached ");
        helixController.DestroyLevel();
        helixController.ProceduralGeneration();
        ballController.OnRestartBehavior();


    }

    // Restart current stage
    public void RestartLevel()
    {
        Debug.Log("GameOver");
        if(RestartEvent != null)
        {
            RestartEvent();
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
