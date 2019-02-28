using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public BouncyBallController ballTarget;
    float offsetCamera;
	
    // Use this for initialization
	void Awake () {
        offsetCamera = transform.position.y - ballTarget.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentCameraPosition = transform.position;
        currentCameraPosition.y = ballTarget.transform.position.y + offsetCamera;
        transform.position = currentCameraPosition;
	}
}
