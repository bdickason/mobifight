/* Combat.cs - Handle attacking, blocking, and special moves */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

	public enum AttackState {
		Startup,
		Active,
		Recovery,
		Idle
	};

	// Attacking
	[Header("Combat")]
	public float startup = 0.3f;
	public float active = 0.5f;
	public float recovery = 0.5f;

	public AttackState attackState = AttackState.Idle;
	private float attackStateDuration;

	private Transform[] bodyParts;
	private SpriteRenderer rightArmSprite;

	// Get Input from the player/CPU
	private Controls controls;

	// Get overall state for this fighter
	private Fighter fighter;

	// Use this for initialization
	void Start () {
		bodyParts = GetComponentsInChildren<Transform>();
		foreach(Transform part in bodyParts) {
			if(part.name == "Right Arm") {
				rightArmSprite = part.GetComponent<SpriteRenderer>();
				Idle();
			}
		}

		controls = GetComponent<Controls>();
		fighter = GetComponent<Fighter>();
	}
	
	// Update is called once per frame
	void Update () {
		// Can Player Do Something?
		if(!fighter.busy) {
			if(controls.Punch()) {
				fighter.nextAction = startup + active + recovery;	// Player can't act until the attack is completed
				Startup();
			}
			else if(attackState != AttackState.Idle) {
				Idle();
			}
		}
		else {
			// Player is doing something
			if(attackStateDuration <= 0) {
				// Current Attack State is completed, move to the next step
				switch(attackState) {
					case AttackState.Startup:
						Active();
						break;
					case AttackState.Active:
						Recovery();
						break;
					case AttackState.Recovery:
						Idle();
						break;
				}
			}
			// Elapse one frame for the attack
			attackStateDuration -= Time.deltaTime;
		}
	}

	// Startup - Attack begun but can't hurt other players (hurtbox, no hitbox)
	void Startup() {
		attackState = AttackState.Startup;
		attackStateDuration = startup;
		rightArmSprite.color = Color.white;
	}

	// Active - Attack can hurt other players (hurtbox+hitbox)
	void Active() {
		attackState = AttackState.Active;
		attackStateDuration = active;
		rightArmSprite.color = Color.magenta;
	}

	// Recovery - Attack cannot hurt other players but player can't attack (hurtbox, no hitbox)
	void Recovery() {
		attackState = AttackState.Recovery;
		attackStateDuration = recovery;
		rightArmSprite.color = Color.grey;
	}

	// Idle - Ready to attack
	void Idle() {
		attackState = AttackState.Idle;
		attackStateDuration = 0;
		rightArmSprite.color = Color.green;
	}
}
