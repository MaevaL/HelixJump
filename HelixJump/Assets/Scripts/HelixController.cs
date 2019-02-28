using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour {

    private Vector3 prevInputPos; 
    private Vector3 rotation; //rotation angle
    public float speed = 0.2f;
	// Use this for initialization
	void Awake () {
        rotation = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        //Work with touch
        //Follow mouvement of the mouse or touch
        if (Input.GetMouseButton(0))
        {
            Vector2 curInputPos = Input.mousePosition; //point touch the screen

            if(prevInputPos == Vector3.zero)
            {
                prevInputPos = curInputPos;
            }

            float delta = prevInputPos.x - curInputPos.x; // distance between two positions
            prevInputPos = curInputPos;

            transform.Rotate(Vector3.up * delta * speed );
        }

        if (Input.GetMouseButtonUp(0))
        {
            prevInputPos = Vector3.zero;
        }
	}
}
