using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public RecycleGameObject prefab;

	private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

	private RecycleGameObject CreateInstance(Vector3 pos){

		var clone = GameObject.Instantiate (prefab);
		clone.transform.position = pos;
		//make a new object is under the object pool
		clone.transform.parent = transform;

		poolInstances.Add (clone);

		return clone;
	}

	public RecycleGameObject NextObject(Vector3 pos){
		RecycleGameObject instance = null;

		// Try to find an inactive object in the pool to use
		foreach (var go in poolInstances) {
			if(go.gameObject.activeSelf != true){
				instance = go;
				instance.transform.position = pos;
			}
		}

		//No object found we create new object
		if(instance == null)
			instance = CreateInstance (pos);

		instance.Restart ();

		return instance;
	}

}
