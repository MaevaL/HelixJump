using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncyBallController : MonoBehaviour {

    private Vector3 initialPosition;
    private bool ignoreCollision; // handle case : ball hit two object at the same time
    public Rigidbody rbBall;
    public float force = 6f;

    [Header("Super Ball")]
    public static int perfect = 0;
    public bool isSuperBallActive = false;

    // Use this for initialization
    void Awake () {
        initialPosition = transform.position;
	}

    void Start()
    {
        GameController.instance.RestartEvent += OnRestartBehavior;
        rbBall = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreCollision)
            return;

     
        if (!collision.gameObject.transform.parent.CompareTag("Goal") && isSuperBallActive)
        {

            Debug.Log("SUPER BALL");
            collision.transform.parent.gameObject.SetActive(false);
            
            GameController.instance.AddScore(4);
            isSuperBallActive = false;
           
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            GameController.instance.RestartLevel();
        }


        //Debug.Log("Collision with " + collision.gameObject.name);

        rbBall.velocity = Vector3.zero;
        rbBall.AddForce(Vector3.up * force, ForceMode.Impulse);
        ignoreCollision = true;

        Invoke("AllowCollision", 0.2f);
        perfect = 0;
    }
	
    private void AllowCollision()
    {
        ignoreCollision = false;
    }

    public void OnRestartBehavior()
    {
        transform.position = initialPosition;
    }
	// Update is called once per frame
	void Update () {
		if(perfect >= 2 && !isSuperBallActive)
        {
            isSuperBallActive = true;
            rbBall.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
	}
}
