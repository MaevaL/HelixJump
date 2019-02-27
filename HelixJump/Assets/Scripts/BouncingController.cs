using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingController : MonoBehaviour {

    private Rigidbody ballPhysics;
    public float gravity = -1500f;
    bool isImpulse = false;

    private void Start()
    {
        ballPhysics = GetComponent<Rigidbody>();
        ballPhysics.useGravity = false;
    }

    private void FixedUpdate()
    {
        isImpulse = false;
        ballPhysics.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
    }

    private void Update()
    {
        if (isImpulse) { ApplyImpulse(); }
    }

    void ApplyImpulse()
    {
        isImpulse = false;

        Vector3 velocity = ballPhysics.velocity;
        velocity.y = 5f; //"jump" at 20 meters per second
        ballPhysics.velocity = velocity;
        ballPhysics.AddForce(Vector3.up * 2f , ForceMode.Impulse);
        //ballPhysics.velocity = new Vector3(0, 50f, 0);
    }

    private void OnCollisionEnter()
    {
        isImpulse = true;
    }

}
