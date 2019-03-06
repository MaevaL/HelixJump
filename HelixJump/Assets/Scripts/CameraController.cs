using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public BouncyBallController ballTarget;
    private Vector3 initialPosition;
    float offsetCamera = 1.50f;
	
    // Use this for initialization
	void Awake () {
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 currentCameraPosition = transform.position;
        //currentCameraPosition.y = ballTarget.transform.position.y + offsetCamera;
        //transform.position = currentCameraPosition;

        if(ballTarget.transform.position.y + offsetCamera < transform.position.y)
        {
            Vector3 position = transform.position;
            position.y = ballTarget.transform.position.y + offsetCamera;
            transform.position = position;
        }
	}

    public void OnRestartBehavior()
    {
        transform.position = initialPosition;
    }
}
