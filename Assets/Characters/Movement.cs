/* Movement.cs - Moves a player around the scene */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
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

	// Get Input from the player/CPU
	private Controls controls;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

		// Player object gives us player input
		controls = GetComponent<Controls>();
	}

	
	// Update is called once per frame
	void Update () {
		// Jumping
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (controls.Vertical() > 0 && grounded)
        {
            jump = true;
        }
	}

	void FixedUpdate() {
		float h = controls.Horizontal();

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
