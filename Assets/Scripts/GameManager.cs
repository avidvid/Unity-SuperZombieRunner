using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;

	//Blinking Continue text vars
	public Text continueText;
	private float blinkTime = 0f;
	private bool blink;

	//Blinking score text vars
	public Text scoreText;
	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private bool beatBestTime;

	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;

	void Awake(){
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
	}

	// Use this for initialization
	void Start () {

		//Position the floor at the end of sceen
		var floorHeight = floor.transform.localScale.y;
		var pos = floor.transform.position;
		pos.x = 0;
		//Bottom of the screen + center of the floor height
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
		floor.transform.position = pos;



		spawner.active = false;

		//moved this to update ResetGame();
		Time.timeScale = 0;

		continueText.text = "PRESS ANY BUTTON TO START";

		bestTime = PlayerPrefs.GetFloat ("BestTime");
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameStarted && Time.timeScale == 0) {

			if(Input.anyKeyDown){

				timeManager.ManipulateTime(1, 1f);
				ResetGame();
			}
		}


		//Regular blink: use time delta for blink but for our came the time stops and deltatime is 0 

		if (!gameStarted) {
			blinkTime ++;
			if (blinkTime % 40 == 0) {
				blink = !blink;
			}
			//Reverse the value of blink
			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);

			var textColor = beatBestTime ? "#FF0" : "#FFF";

			scoreText.text = "TIME: " + FormatTime (timeElapsed) + "\n<color="+textColor+">BEST: " + FormatTime (bestTime)+"</color>";
		} else {
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: "+FormatTime(timeElapsed);
		}
	}

	void OnPlayerKilled(){
		spawner.active = false;

		//For garbage collection and making sure distroy functions works properly we need to remove the link between the methods 
		//The linkage have been added in ResetGame
		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		//function in time manager
		timeManager.ManipulateTime (0, 5.5f);
		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";

		if (timeElapsed > bestTime) {
			bestTime = timeElapsed;
			//WORKS LIKE COOKIES IN UNITY 
			PlayerPrefs.SetFloat("BestTime", bestTime);
			beatBestTime = true;
		}
	}

	void ResetGame(){
		spawner.active = true;

		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height/PixelPerfectCamera.pixelsToUnits)/2 + 100, 0));

		//Player DestroyOffscreen DestroyCallback function will be loaded with OnPlayerKilled from game manager 
		//we should remove The linkage in OnPlayerKilled
		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;

		gameStarted = true;

		//Hide the text when the game starts 
		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0;
		beatBestTime = false;
	}

	string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);
		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
	}

}
