using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public void ManipulateTime(float newTime, float duration){

		//We need this to have let the rederer and loops run and execute correctly befor making more changes
		if (Time.timeScale == 0)
			Time.timeScale = 0.1f;

		StartCoroutine (FadeTo (newTime, duration));
	}

	IEnumerator FadeTo(float newTime, float duration){

		for (float t = 0f; t < 1; t += Time.deltaTime / duration) {

			//lerp change value1 to value 2 direction with the speed of t 
			Time.timeScale = Mathf.Lerp(Time.timeScale, newTime, t);

			if(Mathf.Abs(newTime - Time.timeScale) < .01f){
				Time.timeScale = newTime;
				yield break;
			}
			// to let this executed over multiple frames 
			yield return null;
		} 
	}

}
