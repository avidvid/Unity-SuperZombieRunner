using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator animator;
	private InputState inputState;

	void Awake(){
		animator = GetComponent<Animator> ();
		inputState = GetComponent<InputState> ();
	}

	// Update is called once per frame
	void Update () {
	
		var running = true;

		//Player dragged off the screen and he is not in the jump mode 
		if (inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold)
			running = false;

		animator.SetBool ("Running", running);
	}
}
