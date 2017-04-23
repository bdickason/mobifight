/* Controls.cs - Define which players are playing and handle their input */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public enum users {
		Computer, Player1, Player2
	};
	
	public enum trainingCommands {
		// Used to tell the CPU to send certain inputs constantly
		Stand, Jump, Crouch
	};

	// Who is playing? (Input)
	[Header("Player Controls")]
	public users player = users.Player1;
	public trainingCommands command = trainingCommands.Stand;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	// Arrow keys / Joystick
	public float Vertical() {
		if(player != users.Computer) {
			return(Input.GetAxis(player + "Vertical"));
		}
		else {
			if(command == trainingCommands.Jump) {
				return(1);
			}
			else if(command == trainingCommands.Crouch) {
				return(-1);
			}
			else {
				// Stand Still
				return(0);
			}
		}		
	}

	public float Horizontal() {
		return(Input.GetAxis(player + "Horizontal"));
	}

	// Buttons
	public bool Punch() {
		return(Input.GetButtonDown(player + "Punch"));
	}

}
