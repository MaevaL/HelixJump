using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]private Text scoreText;
    [SerializeField]private Text highScoreText;

    void Start()
    {
        highScoreText.text = "Meilleur : " + GameController.instance.highscore.ToString();
    }
	// Update is called once per frame
	void Update () {
        scoreText.text = GameController.instance.currentScore.ToString();
        if (GameController.instance.isHighscoreReach)
        {
            highScoreText.text = "Meilleur : " + PlayerPrefs.GetInt("Highscore");
        }
	}
}
