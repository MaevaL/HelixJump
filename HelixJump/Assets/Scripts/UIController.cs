using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]private Text scoreText;
    [SerializeField]private Text highScoreText;
    [SerializeField] private Text level;
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NextLevelPanel;
   public GameObject localScaleText;
    [SerializeField] private Canvas canvas;

    public delegate void RestartEventHandler();
    public static event RestartEventHandler RestartEvent;

    void Start()
    {
        highScoreText.text = "Best : " + GameController.instance.highscore.ToString();
    }
	// Update is called once per frame
	void Update () {
        scoreText.text = GameController.instance.currentScore.ToString();
        if (GameController.instance.isHighscoreReach)
        {
            highScoreText.text = "Best : " + PlayerPrefs.GetInt("Highscore");
        }
    }
    private void LevelSucceed()
    {
        ScorePanel.SetActive(false);
        level.text = "Level " + GameController.instance.level + " passed";
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
    }

    private void GameOver()
    {
        GameController.instance.IsGameOver = true;
        ScorePanel.SetActive(false);
        GameOverPanel.SetActive(true); 
    }

    public void OnGameOver()
    {
        Invoke("GameOver", 0.1f);       
    }

    public void OnClickPanel()
    {
        GameOverPanel.SetActive(false);
        GameController.instance.IsGameOver = false;
        if (RestartEvent != null)
        {
            RestartEvent();
        }
       
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
