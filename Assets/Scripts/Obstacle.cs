using UnityEngine;
using System.Collections;


//Because it has interface it should implement Restart & Shutdown
public class Obstacle : MonoBehaviour, IRecyle {

	public Sprite[] sprites;
	public Vector2 colliderOffset = Vector2.zero;

	public void Restart(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprites [Random.Range (0, sprites.Length)];

		//Adjust the collider size for each object
		var collider = GetComponent<BoxCollider2D> ();
		var size = renderer.bounds.size;
		
		//For each new Object Recenter the box collider for other objects  
		size.y += colliderOffset.y;

		collider.size = size;
		//Set the collier offset based on the object 
		collider.offset = new Vector2 (-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);

	}

	public void Shutdown(){

	}
}
