/* Combat.cs - Handle attacking, blocking, and special moves */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
	// Attacking
	[Header("Combat")]
	public float startup = 0.5f;
	public float active = 20f;
	public float recovery = 10f;

	private bool isAttacking = false;
	private float nextAction = 0f;

	private Transform[] bodyParts;
	private SpriteRenderer rightArmSprite;

	// Get Input from the player/CPU
	private Controls controls;

	// Use this for initialization
	void Start () {
		bodyParts = GetComponentsInChildren<Transform>();
		foreach(Transform part in bodyParts) {
			if(part.name == "Right arm") {
				rightArmSprite = part.GetComponent<SpriteRenderer>();
			}
		}

		// Player object gives us player input
		controls = GetComponent<Controls>();
	}
	
	// Update is called once per frame
	void Update () {
		// Attacks
		// Can Player Do Something?
		if(!isAttacking) {
			if(controls.Punch()) {
				nextAction += startup;
			}
		}

		// Elapse one frame
		if(nextAction > 0f) {
			isAttacking = true;
			nextAction -= Time.deltaTime;

			rightArmSprite.color = Color.red;
		}
		else {
			// Player can take another action
			isAttacking = false;
			rightArmSprite.color = Color.green;
		}
	}
}
