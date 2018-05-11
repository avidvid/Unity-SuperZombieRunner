using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

	//Value of pixels off screen to destroy an object 
	public float offset = 16f;
	
	private bool offscreen;
	private float offscreenX = 0;

	//connecting one property with a method so it lets os to connect one script to another one 
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	private Rigidbody2D body2d;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		offscreenX = (Screen.width / PixelPerfectCamera.pixelsToUnits) / 2 + offset;
	}
	
	// Update is called once per frame
	void Update () {

		var posX = transform.position.x;
		var dirX = body2d.velocity.x;

		if (Mathf.Abs (posX) > offscreenX) {
			// If it is off screen and leaving the sceen
			if (dirX < 0 && posX < -offscreenX) {
				offscreen = true;
			} else if (dirX > 0 && posX > offscreenX) {
				offscreen = true;
			}

		} else {
			offscreen = false;
		}

		if (offscreen) {
			OnOutOfBounds();
		}

	}


	public void OnOutOfBounds(){
		offscreen = false;
		GameObjectUtil.Destroy (gameObject);


		//this will be define in the game manager 
		if (DestroyCallback != null) {
			DestroyCallback();
		}
	}
}
