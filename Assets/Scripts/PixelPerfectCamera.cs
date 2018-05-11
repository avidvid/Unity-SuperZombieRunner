using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

	//changig the size of the camera to feet the screen 
	public static float pixelsToUnits = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution = new Vector2 (240, 160);

	// Use this for before initialization
	void Awake () {
		var camera = GetComponent<Camera> ();
		if (camera.orthographic) { //In 2d mode this is set
			scale = Screen.height / nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;
		}
	}
}
