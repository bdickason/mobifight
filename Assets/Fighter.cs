/* Fighter - Contains overall information about the fighter's state */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
	
	public float nextAction = 0f;
	public bool busy = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(nextAction > 0f) {
			// Elapse one frame
			nextAction -= Time.deltaTime;
			busy = true;
		}
		else {
			busy = false;
		}
	}
}
