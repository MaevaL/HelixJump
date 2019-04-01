using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    [HideInInspector]
    public int highscore;
    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int currentLevel = 1;

    [Header("Controllers")]
    public HelixController helixController;
    public BouncyBallController ballController;
    public UIController uiController;
    public CameraController cameraController;
    

    [HideInInspector]
    public bool isGameOver = false;

    private bool isHighscoreReach = false;

    void Awake () {

        //Singleton
		if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //Load player prefs saving
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        }

        if (PlayerPrefs.HasKey("Level"))
        {
            currentLevel = PlayerPrefs.GetInt("Level");
        }

        if(currentLevel == 0)
        {
            currentLevel++;
        }
    }

    /// <summary>
    /// Behavior when a level is passed
    /// </summary>
    public void NextLevel()
    {
        if (!isGameOver)
        {
            currentLevel++;
            PlayerPrefs.SetInt("Level", currentLevel);

            uiController.OnLevelSucceed();
            helixController.DestroyLevel();
            helixController.ProceduralGeneration();
            ballController.OnRestartBehavior();
            cameraController.OnRestartBehavior();
            HelixStep.OnRestartBehavior();

            currentScore = 0;
        }

    }

    /// <summary>
    /// Behavior when a level is restart because of a game over
    /// </summary>
    public void OnRestartBehavior()
    {
        helixController.OnRestartBehavior();
        ballController.OnRestartBehavior();
        cameraController.OnRestartBehavior();
        HelixStep.OnRestartBehavior();
        currentScore = 0;
    }

    /// <summary>
    /// Active gameOver UI
    /// </summary>
    public void GameOver()
    {
        uiController.OnGameOver();        
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

    /// <summary>
    /// Launch a coroutine for a platform animation
    /// </summary>
    /// <param name="platform">platform concerned</param>
    public void LaunchAnimationPlatform(GameObject platform)
    {
        StartCoroutine(AnimationPlatform(platform));
    }

    /// <summary>
    /// Execute a platform animation
    /// </summary>
    /// <param name="platfrom">platform to animate</param>
    /// <returns></returns>
    private IEnumerator AnimationPlatform(GameObject platfrom)
    {
        float count = 0;
        while (count < 1)
        {
            if(platfrom == null) { yield return null;  }
            else
            {
                platfrom.transform.localScale = Vector3.Lerp(platfrom.transform.localScale, Vector3.up * 2, count);
                count += 0.1f;
                yield return new WaitForSeconds(0.01f);
            }
       
        }

    }

   



}
