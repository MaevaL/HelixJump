using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixStep : MonoBehaviour {

    private static int step = 0;

	void OnTriggerEnter(Collider other)
    {        
        step++;
        GameController.instance.AddScore(2);
        BouncyBallController.perfect++;
        
    }
}
