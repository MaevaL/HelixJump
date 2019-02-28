using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        GameController.instance.NextLevel();
    }
}
