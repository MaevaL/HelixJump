using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BouncyBallController : MonoBehaviour {

    private Vector3 initialPosition;
    private bool ignoreCollision; // handle case : ball hit two object at the same time
    public Rigidbody rbBall;
    public float force = 6f;
    private bool isGameOver = false;
    [Header("Super Ball")]
    public static int perfect = 0;
    public bool isSuperBallActive = false;
    public int currentStep;
    public UIController uiController;
    public bool hasAddingScore = false;
    public GameObject poof;
    Vector3 ballLocalScale;
    AudioSource audio;

    // Use this for initialization
    void Awake () {
        initialPosition = transform.position;
        HelixStep.StepEvent += OnStepEvent;
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        rbBall = GetComponent<Rigidbody>();
        ballLocalScale = transform.localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreCollision)
            return;

        audio.Play();
        
     
        if (!collision.gameObject.transform.parent.CompareTag("Goal") && isSuperBallActive && !GameController.instance.IsGameOver)
        {
            GameObject go = Instantiate(poof, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            //TO REPLACE
             StartCoroutine(animationPlatform(collision.transform.parent.gameObject));
            //// collision.transform.parent.gameObject.SetActive(false);


            GameController.instance.AddScore(perfect);
            isSuperBallActive = false;
           
        }
         else if (collision.gameObject.CompareTag("Trap"))
        {
            rbBall.isKinematic = true;
            rbBall.transform.localScale = new Vector3(rbBall.transform.localScale.x,0.1f, rbBall.transform.localScale.z);
            GameController.instance.GameOver();
            perfect = 0;
           
        }

        if (!GameController.instance.IsGameOver)
        {
            
            Instantiate(poof, transform.position, Quaternion.identity);
            if (!hasAddingScore)
            {
                GameController.instance.AddScore(2);
                Invoke("DesactiveLocalScore", 1f);
                uiController.localScaleText.GetComponent<Text>().text = "+" + 2;
                uiController.localScaleText.SetActive(true);
                hasAddingScore = true;
            }
            rbBall.velocity = Vector3.zero;
            rbBall.AddForce(Vector3.up * force, ForceMode.Impulse);
            ignoreCollision = true;
              
            Invoke("AllowCollision", 0.2f);
        
           
        }
        perfect = 0;
    }
	
   
    private void AllowCollision()
    {
        ignoreCollision = false;
    }

    public void OnRestartBehavior()
    {
        transform.localScale = ballLocalScale;
        transform.position = initialPosition;
        perfect = 0;
        rbBall.isKinematic = false;
    }

	// Update is called once per frame
	void Update () {
        if (perfect >= 3 && !isSuperBallActive && !GameController.instance.IsGameOver)
        {
            isSuperBallActive = true;
            rbBall.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
	}

    void OnStepEvent()
    {
        hasAddingScore = false;
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

    void DesactiveLocalScore()
    {
        uiController.localScaleText.SetActive(false);
    }

}
