using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpSpeed = 250f;
	public float forwardSpeed = 20;

	private Rigidbody2D body2d;
	private InputState inputState;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}

	// Update is called once per frame
	void Update () {

		if (inputState.standing) {
			if(inputState.actionButton){
				//transform.position.x < 0 means if the player is left side of the screen 0 is the center 
				body2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
			}
		}

	}
}
