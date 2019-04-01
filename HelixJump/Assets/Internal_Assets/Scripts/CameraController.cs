using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public BouncyBallController ballTarget;
    private Vector3 initialPosition;
    private float offsetCamera = 2.3f;
	
	void Awake () {
        initialPosition = transform.position;
    }
	
	void Update () {

        if(ballTarget.transform.position.y + offsetCamera < transform.position.y)
        {
            Vector3 position = transform.position;
            position.y = ballTarget.transform.position.y + offsetCamera;
            transform.position = position;
        }
	}

    /// <summary>
    /// Camera is put on its initial position when a level is restarted
    /// </summary>
    public void OnRestartBehavior()
    {
        transform.position = initialPosition;
    }
}
