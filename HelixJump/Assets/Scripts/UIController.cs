using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]private Text scoreText;
    [SerializeField]private Text highScoreText;
    [SerializeField] private Text level;
    [SerializeField]
    private Text PercentFinished;
    [SerializeField]
    private Text LevelGoText;
    [SerializeField]
    private Text CurrentLevelText;

    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NextLevelPanel;
   public GameObject localScaleText;
    [SerializeField] private Canvas canvas;

    void Start()
    {
        highScoreText.text = "Best : " + GameController.instance.highscore.ToString();
        CurrentLevelText.text = "Level " + GameController.instance.currentLevel;
    }

	// Update is called once per frame
	void Update () {
        scoreText.text = GameController.instance.currentScore.ToString();
        if(HelixStep.step > 0)
        {
            highScoreText.gameObject.SetActive(false);
        } else
        {
            highScoreText.gameObject.SetActive(true);
        }
    }
    private void LevelSucceed()
    {
        ScorePanel.SetActive(false);
        level.text = "Level " + GameController.instance.currentLevel + " passed";
        CurrentLevelText.text = "Level " + GameController.instance.currentLevel;
        NextLevelPanel.SetActive(true);
    }

    private void ReleaseLevelPan()
    {
        NextLevelPanel.SetActive(false);
        ScorePanel.SetActive(true);
    }

    public void OnLevelSucceed()
    {
        Invoke("LevelSucceed", 0.2f);
        Invoke("ReleaseLevelPan", 1.2f);
        highScoreText.text = "Best : " + GameController.instance.highscore;
        localScaleText.SetActive(false);
    }

    private void GameOver()
    {
        GameController.instance.isGameOver = true;
        LevelGoText.text = "Level " + GameController.instance.currentLevel;
        PercentFinished.text = "Ended " + HelixStep.step * 10 + "%";
        GameOverPanel.SetActive(true);
        highScoreText.text = "Best : " + GameController.instance.highscore;
    }

    public void OnGameOver()
    {
        Invoke("GameOver", 0.1f);       
    }

    public void OnClickPanel()
    {
        GameOverPanel.SetActive(false);
        
        ScorePanel.SetActive(true);
        GameController.instance.isGameOver = false;
        GameController.instance.OnRestartBehavior();
       
    }

    public void ScoreText(int score, Vector3 position)
    {
        GameObject localScore = new GameObject("ScoreText");
        localScore.AddComponent<Text>();
        localScore.GetComponent<Text>().text = "+" + score;
        localScore.GetComponent<Text>().fontSize = 100;
        localScore.transform.position = position;
        localScore.transform.parent = canvas.transform;
    }

    
}
