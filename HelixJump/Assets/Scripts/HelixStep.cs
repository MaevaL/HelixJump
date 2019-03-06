using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelixStep : MonoBehaviour {

    public static int step;
    public delegate void StepEventHandler();
    public static event StepEventHandler StepEvent;
    public UIController uiController;
    AudioSource audio;

    void Awake()
    {
        step = 0;

    }

    void Start()
    {
        audio = transform.parent.transform.parent.GetComponent<AudioSource>();
    }
	void OnTriggerEnter(Collider other)
    {        
        step += 1;
        if(StepEvent != null)
        {
            StepEvent();
        }

        audio.Play();
        StartCoroutine(animationPlatform(transform.gameObject));
        
        Debug.Log("perfect : " + BouncyBallController.perfect);
        BouncyBallController.perfect++;       
    }

    IEnumerator animationPlatform(GameObject go)
    {
        Debug.Log("Coroutine");
        float count = 0;
        while (count < 1)
        {
            go.transform.localScale = Vector3.Lerp(go.transform.localScale, Vector3.up * 2, count);
            count += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
