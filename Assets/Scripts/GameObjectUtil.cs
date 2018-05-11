using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//We don't need MonoBehaviour because this is just an static class for  other classes to use 

public class GameObjectUtil {

	//Identify the type of pool by the key and value 
	private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool> ();

	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;
		//if the prefab (obj) is recyclable use the pool 
		var recycledScript = prefab.GetComponent<RecycleGameObject> ();
		if (recycledScript != null) {
			//Get the pool 
			var pool = GetObjectPool (recycledScript);
			//get the instance available (create/inactive)
			instance = pool.NextObject (pos).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}

	public static void Destroy(GameObject gameObject){

		var recyleGameObject = gameObject.GetComponent<RecycleGameObject> ();

		if (recyleGameObject != null) {
			recyleGameObject.Shutdown ();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	private static ObjectPool GetObjectPool(RecycleGameObject reference){
		ObjectPool pool = null;
		//if we find the pool we return it
		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else {
			//we need to create new pool
			var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
			pool = poolContainer.AddComponent<ObjectPool>();
			pool.prefab = reference;
			pools.Add (reference, pool);
		}

		return pool;
	}

}
