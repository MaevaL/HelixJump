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
    public UIController uiController;
    public GameObject poof;
    Vector3 ballLocalScale;
    SoundController sounds;

    // Use this for initialization
    void Awake () {
        initialPosition = transform.position;
    }

    void Start()
    {
        rbBall = GetComponent<Rigidbody>();
        ballLocalScale = transform.localScale;
        sounds = SoundController.instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Animation>().Play();

        if (ignoreCollision)
            return;

        sounds.PlaySingle(sounds.BounceClip);

        HelixStep.stepNoCollision = 0;
        
        if (!collision.gameObject.transform.parent.CompareTag("Goal") && isSuperBallActive && !GameController.instance.isGameOver)
        {
            GameObject go = Instantiate(poof, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

          
            GameController.instance.LaunchAnimationPlatform(collision.transform.parent.gameObject);

            isSuperBallActive = false;
           
        }
         else if (collision.gameObject.CompareTag("Trap"))
        {
            rbBall.isKinematic = true;
            rbBall.transform.localScale = new Vector3(rbBall.transform.localScale.x,0.1f, rbBall.transform.localScale.z);
            GameController.instance.GameOver();
        }

        if (!GameController.instance.isGameOver)
        {            
            Instantiate(poof, transform.position, Quaternion.identity);
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
        isSuperBallActive = false;
        rbBall.isKinematic = false;
    }

	// Update is called once per frame
	void Update () {
        if (perfect >= 3 && !isSuperBallActive && !GameController.instance.isGameOver)
        {
            isSuperBallActive = true;
            rbBall.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
	}

}
