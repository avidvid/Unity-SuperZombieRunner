using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//interface: contract that ensure the classes that using it will always have the same public methods 
public interface IRecyle{
	void Restart();
	void Shutdown();
}

public class RecycleGameObject : MonoBehaviour {

	private List<IRecyle> recycleComponents;

	void Awake(){
		//Getting ol the object
		var components = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecyle> ();
		//Check if the impliment IRecyle 
		foreach (var component in components) {
			if(component is IRecyle){
				recycleComponents.Add (component as IRecyle);
			}
		}
		
		Debug.Log(name+ " Found "+ recycleComponents.Count + " Components ");
	}


	public void Restart(){
		gameObject.SetActive (true);

		foreach (var component in recycleComponents) {
			component.Restart();
		}
	}

	public void Shutdown(){
		gameObject.SetActive (false);

		foreach (var component in recycleComponents) {
			component.Shutdown();
		}
	}

}
