using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingController : MonoBehaviour {

    public float jumpHeight = 4;
    public float TimeToJumpApex = 0.4f;
    float gravity;
    float moveSpeed = 6;
    float jumpVelocity;
    float velocitySmoothing;

    float groundedAcceleration = 0.2f;
    float airAcceleration = 0.1f;

    public Vector3 previousPosition;

    public Vector3 velocity;
    public LayerMask layerMask;

    public Vector3 origin;
    public Vector3 direction;

    public float maxDistance = 0.5f;
    public float currentHitdistance;
    bool isColliding = false;

    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * TimeToJumpApex;
        Debug.Log("gravity :" + gravity);
        Debug.Log("jumpVelocity :" + jumpVelocity);

        Debug.Log(GetComponent<SphereCollider>().radius);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(origin, 0.25f, Vector3.down, out hit, maxDistance, layerMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log(hit.transform.gameObject.name);
            currentHitdistance = hit.distance;
            Debug.Log(currentHitdistance);

            //velocity.y = (hit.distance/2f - 0.01f) * -1f;
            //isColliding = false;


        } 

           // currentHitdistance = maxDistance;

            ApplyGravity();
        if (isColliding)
        {
            velocity.y = jumpVelocity;
        }
       
        transform.position += velocity * Time.deltaTime;

    }

    void Update()
    {
        previousPosition = transform.position;
        origin = previousPosition;    
    }

    void ApplyGravity()
    {
            velocity.y += gravity * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("isColliding");
        isColliding = true;
        velocity.y = 0;
    }

    void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, 0.25f + 0.05f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, (origin + Vector3.down * currentHitdistance));
    }

}
