using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelixStep : MonoBehaviour {

    public static int stepNoCollision;
    public static int step;
    public delegate void StepEventHandler();
    public static event StepEventHandler StepEvent;
    public UIController uiController;
    SoundController sounds;

    void Awake()
    {
        stepNoCollision = 0;
        step = 0;

    }

    void Start()
    {
        uiController = transform.parent.transform.parent.GetComponent<HelixController>().uiController;
        sounds = SoundController.instance;
    }
	void OnTriggerEnter(Collider other)
    {        
        stepNoCollision += 1;
        step += 1;

        sounds.PlaySingle(sounds.StepClip);
        //StartCoroutine(animationPlatform(transform.gameObject));
        GameController.instance.LaunchAnimationPlatform(transform.gameObject);

        GameController.instance.AddScore(GameController.instance.currentLevel * stepNoCollision);
        Invoke("DesactiveLocalScore", 1f);
        uiController.localScaleText.GetComponent<Text>().text = "+" + (GameController.instance.currentLevel * stepNoCollision);
        uiController.localScaleText.SetActive(true);

        Debug.Log("perfect : " + BouncyBallController.perfect);
        BouncyBallController.perfect++;       
    }

    void DesactiveLocalScore()
    {
        uiController.localScaleText.SetActive(false);
    }

    public static void OnRestartBehavior()
    {
        step = 0;
        stepNoCollision = 0;
    }
}
