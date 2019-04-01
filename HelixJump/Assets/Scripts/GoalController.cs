using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {

    /// <summary>
    /// Handle goal trigger 
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter(Collider other)
    {
        GameController.instance.NextLevel();
    }
}
