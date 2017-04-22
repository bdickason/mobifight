using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public enum users {
		Computer, Player1, Player2
	};

	public enum trainingCommands {
		Stand, Jump, Crouch
	};

	// Who is playing? (Input)
	[Header("Player Controls")]
	public users player = users.Player1;
	public trainingCommands command = trainingCommands.Stand;


	// Character movement and physics
	[Header("Character Physics")]
	public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;

	[HideInInspector] public bool jump = false;
    private bool grounded = false;
	private bool facingRight = true;
	
	private Rigidbody2D rb2d;


	// Attacking
	public float startup = 5f;
	public float active = 20f;
	public float recovery = 10f;

	private bool isAttacking = false;
	private float nextAction = 0f;

	private Transform[] bodyParts;
	private SpriteRenderer rightArmSprite;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

		bodyParts = GetComponentsInChildren<Transform>();
		foreach(Transform part in bodyParts) {
			Debug.Log(part.name);
			if(part.name == "Right arm") {
				rightArmSprite = part.GetComponent<SpriteRenderer>();
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		if(player == users.Computer) {
			// Training Mode: Computer player should perform a set of actions
			switch(command) {
				case trainingCommands.Stand:
				break;
				case trainingCommands.Crouch:
				break;
				case trainingCommands.Jump:
					if(grounded) {
						jump = true;
					}
				break;
			}
		}

		// Jumping
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown(player + "Jump") && grounded)
        {
            jump = true;
        }

		// Attacks
		// Can Player Do Something?
		if(!isAttacking) {
			if(Input.GetButtonDown(player + "Punch")) {
				nextAction += startup;
			}
		}

		// Elapse one frame
		if(nextAction > 0f) {
			isAttacking = true;
			nextAction -= Time.deltaTime;

			// Change color to green
			rightArmSprite.color = Color.green;
			Debug.Log(nextAction);
		}
		else {
			isAttacking = false;
			rightArmSprite.color = Color.red;
			// Change color to red
		}
		
	}

	void FixedUpdate() {
		float h = Input.GetAxis(player + "Horizontal");

		if (h * rb2d.velocity.x < maxSpeed) {
            rb2d.AddForce(Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
            rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

        if (h > 0 && !facingRight) {
            Flip ();
		}
        else if (h < 0 && facingRight) {
            Flip ();
		}

		if (jump) {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
	}
	
	void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
