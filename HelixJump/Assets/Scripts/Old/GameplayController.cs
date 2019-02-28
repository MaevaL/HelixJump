using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {
    public float speed = 100f;

    private void OnMouseDrag()
    {
        float rotationX = Input.GetAxis("Mouse X") * Mathf.Deg2Rad * speed;
        transform.Rotate(new Vector3(0,1,0) * -rotationX);
    }
}
